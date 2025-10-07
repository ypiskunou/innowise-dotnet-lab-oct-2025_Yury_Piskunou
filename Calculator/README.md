# Architectural Deep Dive: SOLID Console Calculator

This project is a console-based calculator built as a practical exercise in modern software architecture, Test-Driven Development (TDD), and design patterns using .NET. The primary goal was not just to perform calculations, but to engineer a system that is robust, maintainable, and highly extensible, following professional software development principles.

## Core Philosophy: SOLID and Clean Architecture

The entire solution is designed around the SOLID principles and the concepts of Clean Architecture. The key idea is a strict separation of concerns, enforced by a one-way dependency rule pointing towards the center of the architecture (the `Core` project).

## Project Structure

The solution is physically and logically divided into several projects, ensuring that each layer has a single, clear responsibility.

### `src` - Source Code

*   **`Calculator.Core`**: The heart of the application. This project contains only abstractions (interfaces like `ICalculationService`, `ICalculatorSession`, `ITokenDefinition`) and core business models (`Token`). It has **zero** dependencies on other projects, making it the stable core of the system.

*   **`Calculator.Engine`**: The implementation of all business logic. This layer contains the concrete classes that implement the interfaces from `Core`. It is responsible for parsing, validating, and evaluating expressions. It knows **how** to be a calculator but knows nothing about how it will be presented to the user.

*   **`Calculator.ConsoleApp`**: The outermost layer (Presentation & Composition Root). It is responsible for all user interaction (reading from and writing to the console). Crucially, the `Program.cs` in this project acts as the **Composition Root**, where all the application's components are manually "wired up" (Dependency Injection).

### `tests` - Test Projects

*   **`Calculator.Engine.Tests`**: A comprehensive suite of unit and integration tests for the `Calculator.Engine` layer. Development was driven by TDD, ensuring that all business logic is verified and reliable.

## Architectural Patterns and Decisions

This project is a showcase of several key design patterns and principles:

### 1. Dependency Injection (DI) and Composition Root

The application strictly follows the Dependency Inversion Principle. No class creates its own concrete dependencies. Instead, all dependencies are "injected" through constructors. The `Program.cs` file acts as a manual DI Container, responsible for building the entire object graph. This makes the system highly decoupled and testable.

### 2. Plugin-Based Tokenizer (Open/Closed Principle)

The tokenization process is designed to be fully extensible without modifying existing code.
*   The `ITokenDefinition` interface defines a contract for a single token recognition rule (e.g., how to find a number or an operator).
*   The `RegexTokenizer` class is a SOLID-compliant orchestrator that receives a collection of `ITokenDefinition` "plugins". It dynamically constructs a master Regex from these plugins to parse the input string.
*   To add a new token type (e.g., a `^` power operator), one only needs to create a new `PowerOperatorDefinition` class and add it to the list in `Program.cs`. The tokenizer and calculation service remain **unchanged**.

### 3. Strategy Pattern

To avoid large `switch` statements and adhere to the Open/Closed Principle, arithmetic operations are encapsulated using the Strategy pattern.
*   The `IOperationStrategy` interface defines a contract for performing a calculation.
*   Concrete classes like `AdditionStrategy`, `SubtractionStrategy`, etc., implement this interface.
*   The `CalculationService` uses a dictionary of these strategies to dynamically select and execute the correct operation based on the operator token.

### 4. Command Pattern

To manage the application's state (session) and enable features like Undo, the Command pattern is used.
*   The `ICalculatorSession` (`CalculatorEngine`) is a stateful service that maintains a history of operations.
*   Each calculation is wrapped in a `CalculationCommand` object, which stores the state before and after the operation.
*   The `UndoLast()` method simply pops the last command from the history stack and reverts the state, demonstrating a clean separation of the operation itself from its execution and history management.

### 5. Facade Pattern

The `ICalculationService` acts as a Facade, providing a simple `Evaluate(string expression)` method that hides the complex internal pipeline of tokenization, validation, RPN conversion (Shunting-yard algorithm), and final evaluation.