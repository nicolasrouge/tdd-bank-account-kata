# TDD Banking Kata + SOLID + Strategy & Factory Patterns

Description of the Kata: https://kata-log.rocks/banking-kata

## Completed Work
- Basic account operations: `deposit`, `withdrawal`, and `account statement generation`.
- TDD (Test-Driven Development).
- Basic Error Handling.
In this exercise, I also wanted to explore `SOLID` principles and the `Strategy Design Pattern`.
  - SOLID Principles.
  - Design Patterns added:
    - Factory Pattern.
    - Strategy Pattern: Applied for handling different types of fees.

## In a Real-World Scenario
The correct level of abstraction needs to be thought through well. Here are other points not covered in this Kata:
- Mocking or Fakes (for Unit Tests)
- Tasks / Asynchronous operations
- Implementing persistence
- Creating RESTful API endpoints and Services
- Authentication & Authorization
- Event-Driven Architecture (integration with other services)
- Dockerization & Microservices (depends on the context)
- CI/CD Pipeline
- Performance Optimization
- Monitoring & Logging
- Documentation

## Example of Use of Fees from the Client:

```csharp
var account = new BankAccount(1000, transactionFactory);
var fixedFeeStrategy = new FixedFeeStrategy(10); // $10 fixed fee
var percentageFeeStrategy = new PercentageFeeStrategy(0.02m); // 2% fee

// Use fixed fee strategy
account.Withdraw(100, fixedFeeStrategy);

// Use percentage fee strategy
account.Withdraw(200, percentageFeeStrategy);
```
