# Banking Application

A console-based banking simulation built with C# on .NET. Users can register, log in, deposit/withdraw funds, view transaction history, and close accounts. Data is persisted locally in NDJSON files.

## Features

- **User registration & login** with email/password
- **Savings account** created automatically on sign-up
- **Deposit** and **withdraw** with balance validation
- **Transaction history** per user
- **Close account** support
- **NDJSON file persistence** — data stored in `repository/db/`

## Project Structure

```
BankingApplication/
├── entity/             # Domain models (User, Account, Transaction, enums)
├── repository/         # Data access layer (singleton repositories, NDJSON)
├── service/            # Business logic layer (interfaces + implementations)
├── Program.cs          # Console UI entry point
└── README.md
```

## Running

```bash
dotnet run --project BankingApplication
```
