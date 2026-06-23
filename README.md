# Loan Eligibility & Approval System

A REST API built with **ASP.NET Core 8** that evaluates loan applications against 5 predefined business rules and automatically assigns a status: **Approved**, **Rejected**, or **Under Review**.

---

## Tech Stack

| Category | Technology |
|---|---|
| Backend | ASP.NET Core 8 Web API, C# |
| ORM | Entity Framework Core 8 (Code-First) |
| Database | SQL Server (LocalDB) |
| API Docs | Swagger / OpenAPI |
| Source Control | Git |

---

## Project Structure
LoanEligibilitySystem/

├── Controllers/    → API endpoints (no business logic)
├── Services/       → Eligibility engine, EMI calculator, business logic
├── Repositories/   → Database access layer (EF Core)
├── Models/         → EF Core entity (LoanApplication)
├── DTOs/           → Request and response contracts
├── Data/           → AppDbContext
├── Middleware/     → Global exception handling
└── Migrations/     → EF Core migration history


---

## Setup Instructions

### Prerequisites
- .NET 8 SDK
- SQL Server or SQL Server LocalDB
- dotnet-ef tools: `dotnet tool install --global dotnet-ef`

### Steps

1. Clone the repository:
```bash
   git clone https://github.com/naman-p06/LoanEligibilitySystem.git
   cd LoanEligibilitySystem
```

2. Configure the connection string in `appsettings.json`:
```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=LoanEligibilityDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
```

3. Apply migrations to create the database:
```bash
   dotnet ef database update
```

4. Run the project:
```bash
   dotnet run
```

5. Open Swagger UI:
https://localhost:{port}/swagger

---

## API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/loan/apply` | Submit and evaluate a loan application |
| GET | `/api/loan` | Retrieve all loan applications |
| GET | `/api/loan/{id}` | Retrieve a specific application by ID |
| GET | `/api/loan/search?applicationNo=` | Search by application number (e.g. APP1001) |
| GET | `/api/loan/dashboard` | Aggregate statistics by status |

---

## Sample Request

```json
POST /api/loan/apply
{
  "applicantName": "John Smith",
  "age": 30,
  "monthlyIncome": 60000,
  "employmentType": "Salaried",
  "experienceYears": 3,
  "existingEMI": 5000,
  "loanAmount": 500000,
  "loanTenure": 60,
  "creditScore": 820
}
```

## Sample Response

```json
{
  "applicationNumber": "APP1001",
  "applicantName": "John Smith",
  "loanAmount": 500000,
  "calculatedEMI": 10624.26,
  "status": "Approved",
  "remarks": "Congratulations! Your loan of ₹500000.00 has been approved. Monthly EMI will be ₹10624.26.",
  "appliedDate": "2026-06-19T10:30:00Z"
}
```

---

## Business Rules

| Rule | Condition | Result |
|---|---|---|
| Age Validation | Must be between 21–60 years | Outside range → Rejected |
| Income Validation | Minimum ₹25,000/month | Below threshold → Rejected |
| Experience Validation | Salaried ≥ 1 yr, Self-Employed ≥ 2 yrs | Below threshold → Rejected |
| Credit Score | ≥ 800 → Approved, 700–799 → Under Review, < 700 → Rejected | Determines final status |
| EMI Affordability | Existing EMI + New EMI ≤ 50% of monthly income | Exceeds limit → Rejected |

---

## Assumptions

- Annual interest rate is fixed at **10%** for EMI calculation
- Standard bank EMI formula used: `EMI = P × r × (1+r)^n / ((1+r)^n − 1)`
- Credit score valid range assumed: **300–900**
- Application numbers are auto-generated sequentially: APP1001, APP1002, ...
- Employment type accepts exactly: `Salaried` or `Self-Employed`

---

## Author

Naman — Backend Developer Intern