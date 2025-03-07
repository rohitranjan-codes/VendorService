## VendorService - A Microservice for Vendor Management

**VendorService** is a **.NET 8 Minimal API** designed for managing vendors in an **ERP system**. It follows **Clean Architecture**, **CQRS with MediatR**, and integrates **AutoMapper, FluentValidation, structured logging, and Docker support**.

## Solution Overview
This microservice is structured using **Clean Architecture**, ensuring separation of concerns:

- **API Layer** → Exposes endpoints.
- **Application Layer** → Contains **CQRS handlers, DTOs, validation, and mappings**.
- **Domain Layer** → Defines **core business entities (`Vendor`) and repository contracts**.
- **Infrastructure Layer** → Implements **repositories and in-memory storage**.

## Key Features
- **Minimal API with Clean Architecture**  
- **CQRS with MediatR**  
- **Repository Pattern (`IVendorRepository`)**  
- **FluentValidation & AutoMapper**  
- **Custom Middleware for Correlation ID Logging**  
- **Docker Support**  
- **GitHub Actions Workflow for CI/CD**  

## API Endpoints
| Method | Endpoint           | Description |
|--------|-------------------|-------------|
| `GET`  | `/vendors`        | Get all vendors |
| `GET`  | `/vendors/{id}`   | Get vendor by ID |
| `POST` | `/vendors`        | Create a vendor |
| `PUT`  | `/vendors/{id}`   | Update a vendor |
| `DELETE` | `/vendors/{id}` | Delete a vendor |

## Planned Improvements
- **Increase Unit Testing Coverage**  
- **Use a Database instead of in-memory storage**  
- **Implement JWT Authentication**  
- **Replace Exception Throwing with Result Objects in Commands**

