# Ecommerce API - Clean Architecture & CQRS

A robust and scalable Ecommerce backend built with **ASP.NET Core 9.0**, following **Clean Architecture** principles and the **CQRS (Command Query Responsibility Segregation)** pattern.

## 🚀 Overview

This project provides a comprehensive RESTful API for an ecommerce platform. It is designed with modularity and maintainability in mind, utilizing modern .NET features and industry-standard design patterns.

### Key Features
- **Product Management**: Create, update, and manage products and categories.
- **Order Processing**: Seamless order creation and management.
- **Inventory Tracking**: Real-time inventory movement and stock management.
- **Modern Tech Stack**: Built with .NET 9 for performance and long-term support.

## 🛠 Tech Stack

*   **Backend**: [ASP.NET Core 9.0](https://dotnet.microsoft.com/download/dotnet/9.0)
*   **Data Access**: [Entity Framework Core 9.0](https://docs.microsoft.com/en-us/ef/core/)
*   **Mediator Pattern**: [MediatR](https://github.com/jbogard/MediatR) for CQRS
*   **Validation**: [FluentValidation](https://fluentvalidation.net/) for robust input validation
*   **Mapper**: [AutoMapper](https://automapper.org/) for object-to-object mapping
*   **API Documentation**: [Swagger/OpenAPI 9.0](https://swagger.io/)

## 🏗 Architecture

The project is structured into four main layers (Clean Architecture):

1.  **Ecommerce.CORE**: The innermost layer containing domain entities, enums, interfaces, and value objects. This layer has no dependencies on other layers.
2.  **Ecommerce.APPLICATION**: Contains business logic, MediatR commands/queries, DTOs, and validations. It depends only on the CORE layer.
3.  **Ecommerce.INFRASTRUCTURE**: Handles data access (DbContext, Repositories) and external service integrations. It depends on CORE and APPLICATION.
4.  **Ecommerce.API**: The entry point of the application. It handles HTTP requests, controllers, and dependency injection configuration.

## 🏁 Getting Started

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- A relational database (SQL Server, SQLite, or PostgreSQL - configured in `appsettings.json`)

### Installation & Setup

1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-username/ecommerce-api.git
   cd ecommerce-api
   ```

2. **Update Database**:
   Navigate to the API project and run migrations:
   ```bash
   dotnet ef database update --project Ecommerce.INFRASTRUCTURE --startup-project Ecommerce.API
   ```

3. **Run the API**:
   ```bash
   dotnet run --project Ecommerce.API
   ```

## 📖 API Documentation

Once the API is running, you can explore the endpoints via Swagger UI at:
`http://localhost:5000/` (or the configured port in your environment).

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---
*Built with ❤️ by the Project Management Team*
