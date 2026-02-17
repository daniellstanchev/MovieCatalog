# MoviesCatalog

ASP.NET Core MVC project for managing a movie catalog.

## Features
- Full CRUD operations for Movies, Actors, and Genres
- Many-to-many relationship between Movies and Actors
- Browse movies by genre
- Actor profiles with filmography
- Modern responsive UI with Bootstrap 5

## Technologies
- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- SQL Server LocalDB
- Bootstrap 5
- jQuery Validation

## Setup Instructions
1. Clone the repository
2. Open in Visual Studio
3. Update connection string in `appsettings.json` (if needed)
4. Run `Update-Database` in Package Manager Console
5. Press F5 to run

## Project Structure
- Controllers/ - MVC controllers
- Models/ - Entity classes
- Views/ - Razor views
- Data/ - DbContext
- wwwroot/ - Static files

## Author
Daniel Stanchev
