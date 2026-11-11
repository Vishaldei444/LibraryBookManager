# Library Book Manager

Library Book Manager is a C# Windows Forms desktop application for managing a small book collection. It stores records in a local SQLite database and displays them in a DataGridView.

## Features

- **Add** a book with its title, author, category, and availability.
- **Update** a book by selecting its row and editing its details.
- **Delete** a selected book after confirming the action.
- **Search** by title, author, or category. Clear the search box and click **Search** to show all books.
- **Validate** required fields before adding or updating a record.

## Screenshot

![Library Book Manager showing book details and saved records](./library-book-manager.png)

## Technologies Used

- C# and Windows Forms
- SQLite and the `Microsoft.Data.Sqlite` NuGet package
- DataGridView
- Visual Studio 2022

## Database

The app creates `library.db` and the `Books` table on its first run. Records remain saved after the app closes.

| Column | Description |
|---|---|
| `BookID` | Automatically assigned primary key |
| `Title` | Book title |
| `Author` | Author name |
| `Category` | Book category |
| `Available` | `1` for available and `0` for unavailable |

Database location: `%LOCALAPPDATA%\LibraryBookManager\library.db`

## How to Run

1. Clone or download this repository.
2. Open the solution or project file in Visual Studio 2022 on Windows.
3. Restore NuGet packages if prompted.
4. Press **F5** to run the application.
