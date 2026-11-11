using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace LibraryBookManager
{
    public partial class Form1 : Form
    {
        private readonly string databaseFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "LibraryBookManager");

        private readonly string connectionString;
        private long? selectedBookId;

        public Form1()
        {
            InitializeComponent();

            connectionString = new SqliteConnectionStringBuilder
            {
                DataSource = Path.Combine(databaseFolder, "library.db")
            }.ToString();

            btnAdd.Click += AddBook;
            btnUpdate.Click += UpdateBook;
            btnDelete.Click += DeleteBook;
            btnSearch.Click += SearchBooks;
            dgvBooks.SelectionChanged += SelectBook;
            Shown += InitializeDatabase;

            ResetSelection();
        }

        private void InitializeDatabase(object? sender, EventArgs e)
        {
            RunDatabaseAction(() =>
            {
                Directory.CreateDirectory(databaseFolder);

                using var connection = new SqliteConnection(connectionString);
                connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Books (
                        BookID INTEGER PRIMARY KEY,
                        Title TEXT NOT NULL,
                        Author TEXT NOT NULL,
                        Category TEXT NOT NULL,
                        Available INTEGER NOT NULL CHECK (Available IN (0, 1))
                    );";
                command.ExecuteNonQuery();

                LoadBooks();
            });
        }

        private void LoadBooks(string search = "")
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT BookID, Title, Author, Category, Available
                FROM Books
                WHERE instr(lower(Title), lower($search)) > 0
                   OR instr(lower(Author), lower($search)) > 0
                   OR instr(lower(Category), lower($search)) > 0
                ORDER BY BookID;";

            command.Parameters.AddWithValue("$search", search);

            var books = new DataTable();
            books.Columns.Add("BookID", typeof(long));
            books.Columns.Add("Title", typeof(string));
            books.Columns.Add("Author", typeof(string));
            books.Columns.Add("Category", typeof(string));
            books.Columns.Add("Available", typeof(bool));

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                books.Rows.Add(
                    reader.GetInt64(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetInt64(4) == 1);
            }

            dgvBooks.DataSource = books;
            ResetSelection();
        }

        private void AddBook(object? sender, EventArgs e)
        {
            SaveBook(null);
        }

        private void UpdateBook(object? sender, EventArgs e)
        {
            if (selectedBookId.HasValue)
            {
                SaveBook(selectedBookId.Value);
            }
        }

        private void SaveBook(long? bookId)
        {
            string title = txtTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();
            string category = txtCategory.Text.Trim();

            if (title.Length == 0 || author.Length == 0 || category.Length == 0)
            {
                MessageBox.Show("Please enter the title, author, and category.");
                return;
            }

            RunDatabaseAction(() =>
            {
                using var connection = new SqliteConnection(connectionString);
                connection.Open();

                using var command = connection.CreateCommand();
                if (bookId.HasValue)
                {
                    command.CommandText = @"
                        UPDATE Books
                        SET Title = $title, Author = $author,
                            Category = $category, Available = $available
                        WHERE BookID = $id;";
                    command.Parameters.AddWithValue("$id", bookId.Value);
                }
                else
                {
                    command.CommandText = @"
                        INSERT INTO Books (Title, Author, Category, Available)
                        VALUES ($title, $author, $category, $available);";
                }

                command.Parameters.AddWithValue("$title", title);
                command.Parameters.AddWithValue("$author", author);
                command.Parameters.AddWithValue("$category", category);
                command.Parameters.AddWithValue("$available", chkAvailable.Checked ? 1 : 0);
                command.ExecuteNonQuery();

                txtSearch.Clear();
                LoadBooks();
            });
        }

        private void DeleteBook(object? sender, EventArgs e)
        {
            if (!selectedBookId.HasValue)
            {
                return;
            }

            long bookId = selectedBookId.Value;
            DialogResult answer = MessageBox.Show(
                $"Delete Book #{bookId}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (answer != DialogResult.Yes)
            {
                return;
            }

            RunDatabaseAction(() =>
            {
                using var connection = new SqliteConnection(connectionString);
                connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Books WHERE BookID = $id;";
                command.Parameters.AddWithValue("$id", bookId);
                command.ExecuteNonQuery();

                LoadBooks(txtSearch.Text.Trim());
            });
        }

        private void SearchBooks(object? sender, EventArgs e)
        {
            RunDatabaseAction(() => LoadBooks(txtSearch.Text.Trim()));
        }

        private void SelectBook(object? sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0 ||
                dgvBooks.SelectedRows[0].DataBoundItem is not DataRowView row)
            {
                selectedBookId = null;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                return;
            }

            selectedBookId = (long)row["BookID"];
            txtTitle.Text = (string)row["Title"];
            txtAuthor.Text = (string)row["Author"];
            txtCategory.Text = (string)row["Category"];
            chkAvailable.Checked = (bool)row["Available"];

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void ResetSelection()
        {
            dgvBooks.ClearSelection();
            selectedBookId = null;

            txtTitle.Clear();
            txtAuthor.Clear();
            txtCategory.Clear();
            chkAvailable.Checked = true;

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void RunDatabaseAction(Action action)
        {
            try
            {
                action();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Database File Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Database File Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}