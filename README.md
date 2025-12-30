# 🚀 Social Platform API

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)
![Status](https://img.shields.io/badge/Status-Production%20Ready-success)
![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-blue)
![License](https://img.shields.io/badge/License-MIT-green)

A fully functional, enterprise-grade RESTful API for a social media platform, engineered with **ASP.NET Core 8**. This project demonstrates professional backend development practices, focusing on **Clean Architecture**, **Advanced Security**, and **Scalability**. It features a custom **Stateful JWT Authentication** system with middleware-based token blacklisting for secure logouts.

---

## 🌟 Key Features

### 🛡️ Advanced Security & Authentication
*   **ASP.NET Core Identity:** Robust user management and authentication.
*   **Stateful JWT Implementation:** A hybrid approach where tokens are signed but also tracked in the database (`UserTokens`), allowing for immediate revocation.
*   **Secure Logout & Banning:** 
    *   Custom **Token Blacklisting Middleware** intercepts every request.
    *   Ensures banned users or logged-out sessions are rejected instantly, regardless of token expiration.
*   **Role-Based Access Control (RBAC):** Distinct privileges for **Admin** and **User** roles.

### 🏗️ Architecture & Design Patterns
*   **Clean Architecture (Onion):** Strict separation of concerns (Domain, Application, Infrastructure, API).
*   **Repository Pattern:** Decoupling business logic from data access.
*   **Result Pattern:** Standardized API responses (`Success`, `Message`, `Data`) across all endpoints.
*   **DTOs & AutoMapper:** Efficient data transfer and object mapping.
*   **Data Seeding:** Automated setup of Roles and Super Admin on startup.

### 📱 Core Functionality
*   **User System:** Registration, Login, Profile Management.
*   **Content Management:** Full CRUD for **Posts** and **Comments**.
*   **Analytics:** Endpoints for counting user engagement (comments/posts stats).
*   **Admin Panel:** Endpoints to Ban/Unban users and view ban history.

---

## 🛠️ Tech Stack

| Category | Technology |
| :--- | :--- |
| **Framework** | .NET 8 (ASP.NET Core Web API) |
| **Database** | SQL Server |
| **ORM** | Entity Framework Core (Code-First) |
| **Mapping** | AutoMapper |
| **Documentation** | Swagger / OpenAPI |
| **Security** | JWT Bearer + Custom Middleware |

---

## 📊 Database Schema (ERD)

The system manages complex relationships between Users, Content, and Security Logs.

```mermaid
erDiagram
    USERS ||--o{ POSTS : "creates"
    USERS ||--o{ COMMENTS : "writes"
    USERS ||--o{ REFRESH_TOKENS : "active sessions"
    USERS ||--o{ USER_BANS : "history"
    
    POSTS ||--o{ COMMENTS : "has"
    
    USERS {
        int Id PK
        string UserName
        string Email
        bool IsBanned
        string Role
    }
    POSTS {
        int Id PK
        string Content
        int UserId FK
        datetime CreatedAt
    }
    USER_BANS {
        int Id PK
        int UserId FK
        string Reason
        datetime EndDate
    }
    REFRESH_TOKENS {
        int Id PK
        string Token
        bool IsActive
    }
## 🔐 Security Architecture: The Middleware Pipeline

How the API secures every request and enforces Bans/Logouts instantly:

code
Mermaid
download
content_copy
expand_less
graph TD
    A[Client Request] -->|Bearer Token| B(Authentication Middleware)
    B -->|Validate Signature| C{Signature OK?}
    C -- No --> D[401 Unauthorized]
    C -- Yes --> E(Custom Token Middleware)
    E -->|Check DB| F{Token Active & User Not Banned?}
    F -- No (Logged Out/Banned) --> D
    F -- Yes --> G[Authorization Middleware]
    G --> H[Controller Action]
    
    style E fill:#f96,stroke:#333,stroke-width:2px,color:black
    style F fill:#ff9,stroke:#333,stroke-width:2px,color:black
    style D fill:#f00,stroke:#333,stroke-width:2px,color:white
🚀 Getting Started

Follow these steps to set up the project locally.

1. Configure Database

Update appsettings.json in the API project with your connection string:

code
JSON
download
content_copy
expand_less
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=SocialPlatformDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
2. Run Migrations

Apply the database schema:

code
Bash
download
content_copy
expand_less
dotnet ef database update
3. Run the Application

Start the API. On the first run, the Seeding service will create the default Admin.

code
Bash
download
content_copy
expand_less
dotnet run

Default Super Admin Credentials:
Email: admin@social.com
Password: P@ssw0rd123!

## 🧪 API Endpoints Snapshot
Feature	Method	Endpoint	Description	Auth
Auth	POST	/api/User/login	Login & Get Token	❌
Auth	POST	/api/User/logout	Secure Logout (Revokes Token)	✅
Admin	POST	/api/UserBans	Ban a User (Admin Only)	👮‍♂️
Admin	GET	/api/UserBans/{userId}	Get Ban History	👮‍♂️
Posts	POST	/api/Posts	Create Post	✅
Posts	GET	/api/Posts/search	Search Posts	✅
