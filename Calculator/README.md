# Architectural Overview: Console Calculator

This project is a console-based calculator built not just to perform calculations, but to serve as a practical exercise in modern software architecture and design patterns using .NET. The development follows the Test-Driven Development (TDD) methodology.

## Project Structure

The solution is divided into several projects, each with a single, clear responsibility, adhering to the principles of Clean Architecture and Separation of Concerns.

### `src` - Source Code

*   **`Calculator.Core`**: The innermost layer of the architecture. It contains all the core business models (e.g., `Token`) and abstractions/interfaces (e.g., `ICalculationService`, `ICalculatorSession`). This project has zero external dependencies.

*   **`Calculator.Engine`**: The business logic layer. It contains the concrete implementations of the interfaces defined in `Core`. This includes the expression parser, the calculation services, and the implementation of design patterns like Command and Factory.

*   **`Calculator.ConsoleApp`**: The presentation layer and the application's entry point. It is responsible for handling user input and displaying output. It acts as the "composition root" where all dependencies are wired together.

### `tests` - Test Projects

*   **`Calculator.Engine.Tests`**: A dedicated unit test project for the `Calculator.Engine` layer. It ensures that all business logic is correct and reliable.


## Key Patterns & Principles Used

Test-Driven Development (TDD): Logic is developed by writing failing tests first, then writing the code to make them pass.

SOLID Principles: The design adheres to the five SOLID principles for maintainable and extensible code.

Dependency Injection Principle: High-level modules (like ConsoleApp) depend on abstractions (Core), not on concrete implementations (Engine).

Design Patterns:
* Facade: The CalculatorEngine class acts as a simple interface to the complex underlying parsing and evaluation system.
* Command: Used to encapsulate operations, enabling features like Undo/Redo history.
* Factory Method: Used to decouple the creation of operation objects from the main engine logic.