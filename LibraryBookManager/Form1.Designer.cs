using System.Drawing;
using System.Windows.Forms;

namespace LibraryBookManager
{
    partial class Form1
    {
        private Label lblTitle = null!;
        private Label lblAuthor = null!;
        private Label lblCategory = null!;
        private Label lblSearch = null!;

        private TextBox txtTitle = null!;
        private TextBox txtAuthor = null!;
        private TextBox txtCategory = null!;
        private TextBox txtSearch = null!;

        private CheckBox chkAvailable = null!;
        private Button btnAdd = null!;
        private Button btnUpdate = null!;
        private Button btnDelete = null!;
        private Button btnSearch = null!;
        private DataGridView dgvBooks = null!;

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblAuthor = new Label();
            lblCategory = new Label();
            lblSearch = new Label();

            txtTitle = new TextBox();
            txtAuthor = new TextBox();
            txtCategory = new TextBox();
            txtSearch = new TextBox();

            chkAvailable = new CheckBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnSearch = new Button();
            dgvBooks = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)dgvBooks).BeginInit();
            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(15, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Text = "Title";

            txtTitle.Location = new Point(115, 14);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(250, 23);
            txtTitle.TabIndex = 0;

            lblAuthor.AutoSize = true;
            lblAuthor.Location = new Point(15, 55);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Text = "Author";

            txtAuthor.Location = new Point(115, 51);
            txtAuthor.Name = "txtAuthor";
            txtAuthor.Size = new Size(250, 23);
            txtAuthor.TabIndex = 1;

            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(15, 92);
            lblCategory.Name = "lblCategory";
            lblCategory.Text = "Category";

            txtCategory.Location = new Point(115, 88);
            txtCategory.Name = "txtCategory";
            txtCategory.Size = new Size(250, 23);
            txtCategory.TabIndex = 2;

            chkAvailable.AutoSize = true;
            chkAvailable.Checked = true;
            chkAvailable.Location = new Point(15, 126);
            chkAvailable.Name = "chkAvailable";
            chkAvailable.Text = "Available";
            chkAvailable.TabIndex = 3;

            btnAdd.Location = new Point(15, 160);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(95, 28);
            btnAdd.Text = "Add";
            btnAdd.TabIndex = 4;

            btnUpdate.Location = new Point(125, 160);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(95, 28);
            btnUpdate.Text = "Update";
            btnUpdate.TabIndex = 5;

            btnDelete.Location = new Point(235, 160);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(95, 28);
            btnDelete.Text = "Delete";
            btnDelete.TabIndex = 6;

            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(15, 209);
            lblSearch.Name = "lblSearch";
            lblSearch.Text = "Search books";

            txtSearch.Location = new Point(115, 205);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(215, 23);
            txtSearch.TabIndex = 7;

            btnSearch.Location = new Point(345, 202);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(95, 28);
            btnSearch.Text = "Search";
            btnSearch.TabIndex = 8;

            dgvBooks.Location = new Point(15, 245);
            dgvBooks.Name = "dgvBooks";
            dgvBooks.Size = new Size(750, 205);
            dgvBooks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                | AnchorStyles.Left | AnchorStyles.Right;
            dgvBooks.ReadOnly = true;
            dgvBooks.AllowUserToAddRows = false;
            dgvBooks.AllowUserToDeleteRows = false;
            dgvBooks.MultiSelect = false;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBooks.TabIndex = 9;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(780, 465);
            MinimumSize = new Size(480, 410);
            Name = "Form1";
            Text = "Library Book Manager";

            Controls.Add(lblTitle);
            Controls.Add(txtTitle);
            Controls.Add(lblAuthor);
            Controls.Add(txtAuthor);
            Controls.Add(lblCategory);
            Controls.Add(txtCategory);
            Controls.Add(chkAvailable);
            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(lblSearch);
            Controls.Add(txtSearch);
            Controls.Add(btnSearch);
            Controls.Add(dgvBooks);

            ((System.ComponentModel.ISupportInitialize)dgvBooks).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}