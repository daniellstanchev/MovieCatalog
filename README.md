# 🎬 MoviesCatalog

**MoviesCatalog** is a full-featured ASP.NET Core MVC web application for managing a movie catalog. Users can browse movies, view actor profiles, search by genre, leave reviews, and administrators have access to a dedicated admin panel for user and content management.

---

## 📌 Features

### 👤 Public Users
- Browse movies with pagination
- View movie details (cast, director, rating)
- Search movies by title
- Browse actors and view their filmography
- Browse movies by genre
- View user reviews

### 🔐 Authenticated Users
- Login/Logout with ASP.NET Core Identity
- Leave and manage movie reviews

### 👑 Admin Users
- Full CRUD for Movies, Actors, Directors, Genres
- Manage all user reviews
- Manage user roles (Make/Remove Admin)
- Dashboard with statistics
- Admin Area isolated with MVC Areas

### ⚙️ Technical Features
- AJAX live search for actors
- Pagination for movie listings
- 404 Not Found and 500 Error pages
- Average rating calculation from reviews
- Responsive design with Bootstrap 5
- Server-side and client-side validation
- AntiForgeryToken protection
- XSS protection (HTML escaping)

---

## 🛠 Technologies

| Technology | Version |
|------------|---------|
| ASP.NET Core MVC | .NET 8 |
| Entity Framework Core | 8.0 |
| SQL Server LocalDB | - |
| Bootstrap 5 | 5.3 |
| jQuery | 3.6 |
| xUnit | 2.5 |
| Moq | 4.20 |

---

## 📁 Project Structure
MoviesCatalog/
├── Areas/Admin/ # Admin Area (isolated)
│ ├── Controllers/ # Admin controllers
│ └── Views/ # Admin views
├── Controllers/ # MVC Controllers
├── Data/ # DbContext and seeding
├── Migrations/ # EF Core migrations
├── Models/ # Entity models
├── Views/ # Razor views
├── wwwroot/ # Static files (CSS, JS, libs)
├── Program.cs # Application entry point
├── appsettings.json # Configuration
└── MoviesCatalog.csproj # Project file

