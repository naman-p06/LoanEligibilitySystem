IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [LoanApplications] (
    [ApplicationId] int NOT NULL IDENTITY,
    [ApplicationNumber] nvarchar(20) NOT NULL,
    [ApplicantName] nvarchar(100) NOT NULL,
    [Age] int NOT NULL,
    [MonthlyIncome] decimal(18,2) NOT NULL,
    [EmploymentType] nvarchar(20) NOT NULL,
    [ExperienceYears] float NOT NULL,
    [ExistingEMI] decimal(18,2) NOT NULL,
    [LoanAmount] decimal(18,2) NOT NULL,
    [LoanTenure] int NOT NULL,
    [CreditScore] int NOT NULL,
    [Status] nvarchar(20) NOT NULL,
    [Remarks] nvarchar(500) NOT NULL,
    [AppliedDate] datetime2 NOT NULL,
    CONSTRAINT [PK_LoanApplications] PRIMARY KEY ([ApplicationId])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260616121216_InitialCreate', N'10.0.9');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [LoanApplications] ADD [CalculatedEMI] decimal(18,2) NOT NULL DEFAULT 0.0;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260618162515_AddCalculatedEMI', N'10.0.9');

COMMIT;
GO

