# Innowise .NET Pre-Trainee Journal

Welcome to my portfolio repository! This repository chronicles my progress and contains all the projects I developed during the intensive two-month .NET Pre-Trainee program at Innowise. The goal of this journey is to master the fundamentals of modern backend development with the .NET ecosystem and prepare for a software engineering role.

Each project is designed to build upon the knowledge from the previous week, covering a wide range of essential topics.

## Course Structure & Key Topics

The program is structured week-by-week to cover the entire development stack:

*   **Week 1: C# and .NET Fundamentals**
    *   Object-Oriented Programming (OOP)
    *   SOLID Principles, DRY, KISS
    *   Asynchronous Programming (`async`/`await`)
    *   Unit Testing Basics (AAA Pattern)

*   **Week 2: Databases & Data Access**
    *   MS SQL Server and SQL Fundamentals
    *   Dapper Micro-ORM
    *   Repository and Factory Design Patterns

*   **Week 3: Web Development with ASP.NET Core**
    *   RESTful APIs
    *   N-Layer Architecture
    *   HTTP Protocol & Middleware
    *   Dependency Injection (DI)

*   **Week 4: Advanced Data Access & LINQ**
    *   Entity Framework Core
    *   Code-First Approach & Migrations
    *   Language-Integrated Query (LINQ)

## Projects

Below is a list of the key projects completed during the course. Each project is located in its own directory within this repository.

### 1. [Console Calculator](./Calculator/)

A sophisticated console application designed to practice fundamental C# skills and architectural principles.

*   **Key Features:** Parses and evaluates complex mathematical expressions, including parentheses and order of operations.
*   **Technologies & Concepts:** SOLID principles, Layered Architecture, Shunting-yard algorithm, Postfix evaluation, Dependency Injection.

---

### 2. [Async/Await Demo](./AsyncDemo/)

A console application created to demonstrate and compare synchronous and asynchronous programming models in .NET.

*   **Key Features:** Simulates multiple long-running "heavy" tasks, executing them sequentially (synchronously) and in parallel (asynchronously) to highlight the performance benefits and non-blocking nature of `async/await`.
*   **Technologies & Concepts:** Asynchronous Programming, `async`/`await` keywords, `Task.Delay` vs. `Thread.Sleep`, `Task Parallelism`.

---

### 3. [Task Management CLI](./TaskManager/)

A console-based CRUD application for managing a to-do list, interacting with a persistent database.

*   **Key Features:** Create, view, update, and delete tasks.
*   **Technologies & Concepts:** MS SQL Server, Dapper, Repository Pattern, Factory Pattern for DB connections.

---

### 4. [Library Management Web API](./LibraryApi/)

A multi-stage project to build a complete RESTful API for a library system, demonstrating the evolution from simple in-memory storage to a full-fledged database-backed application.

*   **Part 1: In-Memory ASP.NET Core API**
    *   **Features:** Basic CRUD operations for Authors and Books.
    *   **Concepts:** ASP.NET Core Web API basics, Controllers, In-memory data storage, Postman/Swagger for testing.

*   **Part 2: EF Core Integration**
    *   **Features:** Replaced in-memory lists with a persistent database (SQLite or MS SQL LocalDB).
    *   **Concepts:** Entity Framework Core, Code-First migrations, LINQ for data querying, one-to-many relationships.

## Technology Stack

This is a summary of the key technologies and principles I have worked with during this course:

*   **Languages:** C#, SQL
*   **Frameworks:** .NET, ASP.NET Core
*   **Data Access:** Entity Framework Core, Dapper
*   **Databases:** MS SQL Server, SQLite
*   **Architecture & Principles:** SOLID, Clean Architecture, REST, Dependency Injection
*   **Tools:** Git, JetBrains Rider, Postman, Swagger