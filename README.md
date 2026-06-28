# Appointment Booking API

A modular **ASP.NET Core Web API** project built using **Clean Architecture**, **Repository Pattern**, **Service Layer**, and **SQL Server Stored Procedures**.

The project simulates a real-world appointment booking system for clinics and medical centers while following enterprise-level architecture and development practices.

---

## Architecture

The solution is organized into multiple projects to achieve clear separation of concerns.

```
API
Contracts
Infrastructure
MapperLayer
MiddlewareLayer
ValidatorLayer
Modules
    ├── Core
    ├── Auth
    ├── Appointment
    └── Logs
```

---

## Technologies

* ASP.NET Core Web API
* C#
* SQL Server
* ADO.NET
* Stored Procedures
* FluentValidation
* BCrypt
* Dependency Injection
* Swagger / OpenAPI
* Clean Architecture
* Repository Pattern
* Service Layer

---

## Database Design

The database is organized into four schemas.

```
core
auth
appointment
logs
```

### Core

* Persons
* PersonPhoneNumbers

### Auth

* Users
* Roles
* Permissions
* UserRoles
* RolePermissions

### Appointment

* Patients
* PatientNotes
* Specialists
* Services
* SpecialistServices
* AppointmentStatus
* Appointments

### Logs

* ActivityLogs
* ErrorLogs

---

## Features

### Core Module

* Person Management
* Multiple Phone Numbers
* Validation
* Audit Fields

### Authentication

* User Management
* Automatic Username Generation
* BCrypt Password Hashing
* Password Change
* Role Management
* Permission Management

### Appointment Module

* Patient Management
* Specialist Management
* Services
* Appointment Status
* Appointment Scheduling

### Logging

* Activity Logs
* SQL Error Logs

---

## Project Standards

The project follows consistent standards across all layers.

### API

* Controllers
* Request Models
* FluentValidation
* Standardized ApiResponse

### Business Layer

* DTOs
* Services
* Business Rules

### Data Layer

* Repository Pattern
* Stored Procedures only
* ADO.NET

### Database

All Stored Procedures follow the same design:

* SET NOCOUNT ON
* TRY...CATCH
* Validation
* Business Rules
* Activity Logging
* Error Logging
* Audit Fields
* Input Trimming
* No SELECT *
* THROW after logging

---

## Security

* BCrypt password hashing
* PasswordHash is never returned from the API
* Automatic username generation
* Duplicate username prevention

---

## Current Status

Completed modules:

* Core
* Auth (Users)

Additional modules are currently under development.

---

## Project Goals

This project was created as a portfolio project to demonstrate practical backend development skills using enterprise-level architecture and best practices in ASP.NET Core.
