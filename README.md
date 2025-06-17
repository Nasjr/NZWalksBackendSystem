# 🌄 NZWalks API

NZWalks is a RESTful ASP.NET Core Web API built to manage and explore walking trails in New Zealand. It features full **CRUD operations** for walks and regions, supports **user authentication and role-based authorization**, allows **image uploads**, and includes **global exception handling** for cleaner error management.

---

## 📌 Features

- ✅ **Walks CRUD**
- ✅ **Regions CRUD**
- ✅ **User Authentication** using JWT
- ✅ **Role-based Authorization** (`Reader`, `Writer`, `Admin`)
- ✅ **Image Upload** for walk visuals
- ✅ **Global Exception Handling** with clean error responses
- ✅ **Swagger (OpenAPI)** UI for testing and documentation

---

## 🛠️ Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server / SQLite (customizable)
- JWT Authentication
- AutoMapper
- Swagger / Swashbuckle
- FluentValidation (optional for model validation)

---

## 📂 Project Structure

```
NZWalks.API/
│
├── Controllers/
│   ├── WalksController.cs
│   ├── RegionsController.cs
│   └── AuthController.cs
│
├── Repositories/
│   ├── IWalkRepository.cs
│   ├── IRegionRepository.cs
│   └── ITokenRepository.cs
│
├── Models/
│   ├── Domain/
│   ├── DTOs/
│   └── Auth/
│
├── Middleware/
│   └── GlobalExceptionHandler.cs
│
├── Data/
│   └── NZWalksDbContext.cs
│
├── appsettings.json
└── Program.cs / Startup.cs
```

---

## 🔐 Authentication & Authorization

### 🔑 JWT Authentication
Users must log in using `/api/Auth/Login` to receive a JWT token.

### 👥 Roles
- **Reader**: Can view walks and regions.
- **Writer**: Can create, update, and delete.
- **Admin**: Full access.

**Endpoints are protected using `[Authorize(Roles = "...")]`.**

---

## 🖼️ Image Upload

- Upload images via: `POST /api/Image`
- Images are stored in the server's filesystem or cloud storage (customizable).
- Each walk can be linked with an image URL.

---

## 🧪 API Testing with Swagger

Access Swagger UI at:
```
https://localhost:{PORT}/swagger
```

To test protected routes:
- Click **Authorize** and paste the JWT token.

---

## 🧱 Global Exception Handling

Centralized error handling is implemented using custom middleware that catches unhandled exceptions and returns a clean, consistent error response with appropriate status codes.

Example response:
```json
{
  "status": 500,
  "message": "An unexpected error occurred. Please try again later."
}
```

---

## 🚀 Getting Started

### 🔧 Setup Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/NZWalks.git
   ```

2. Configure the database in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "NZWalksConnectionString": "Server=.;Database=NZWalksDb;Trusted_Connection=True;"
     ""AuthDbConnectionString"": "Server=.;Database=NZWalksDb;Trusted_Connection=True;"
   }

     "JWT": {
     "Key": "YOURKEY"
   }
   ```

3. Apply migrations:
   ```bash
   dotnet ef database update
   ```

4. Run the application:
   ```bash
   dotnet run
   ```

---

## 🧑‍💻 Example Users

Seed roles and users using a seeding method or manually in DB.

| Username  | Password     | Roles   |
|-----------|--------------|---------|
| admin     | Admin@123    | Admin   |
| writer    | Writer@123   | Writer  |
| reader    | Reader@123   | Reader  |

---

## 🗂️ Example API Endpoints

- `GET /api/regions`
- `POST /api/regions`
- `GET /api/walks`
- `POST /api/walks`
- `POST /api/Auth/Login`
- `POST /api/Image`

---

## 💡 Future Improvements

- Pagination and filtering
- Refresh tokens
- Image resizing & validation
- Role management interface

---

## 📃 License

MIT License. Feel free to use and modify.
