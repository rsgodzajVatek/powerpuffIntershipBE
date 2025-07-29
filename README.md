# Welcome to Powerpuff Internship Program 2025

# PowerPuff Internship – Backend

## Technologies Used

- **.NET 8**
- **Swashbuckle (Swagger)** – for API documentation
- **REST API**
- **3-layer architecture (Controller → Service → Repository)**
- **Entity Framework Core**

---

## Backend Environment Setup

### 1. Clone the Repository

Clone the backend repository from GitHub:

```bash
git clone https://github.com/rsgodzajVatek/powerpuffIntershipBE.git
```

---
## 2. Build the Project
   Open the solution in Visual Studio and build it using the default settings.

---
## 3 Database Configuration

In the appsettings.json file, make sure your SQL Server instance is properly set up.

The default connection string is:
```json
"PowerPuffDatabase": "Server=.\\SqlExpress;Database=powerpuffIntership;Integrated Security=true;Encrypt=True;TrustServerCertificate=True;Connection Timeout=60;MultipleActiveResultSets=True"
```


🔐 If you're using SQL Server Authentication, replace Integrated Security=true with:
User Id=<your-username>;Password=<your-password>;.

---

## 4 Run the Application : 
Once the application is started, it will automatically:

Apply database migrations:
```C#
using (var serviceScope = app.Services.CreateScope())
{
var context = serviceScope.ServiceProvider.GetRequiredService<PowerPuffDbContext>();
context.Database.Migrate();
}
```

Seed the database with initial data:
```C#
// Database Seed
ServicesContainer.SeedDatabase(app);
```

---
## 5. Registering Application Services
All required services are registered via:
```C#
builder.Services.ConfigureServices();
```

Make sure this call exists in Program.cs.

---

## Swagger API Documentation
Swagger UI is available in development mode at:

https://localhost:7230/swagger

You can use Swagger UI to test API endpoints, including file uploads and more.

---

## Summary
After completing the above steps, your backend will be fully operational with:

Connected and seeded SQL Server database

Swagger UI available for API testing and exploration

Full 3-layer service architecture wired and ready for frontend integration

### You can find more information about 3-layer architecture in this article : 
https://kenslearningcurve.com/tutorials/use-a-3-tier-architecture-with-c/
