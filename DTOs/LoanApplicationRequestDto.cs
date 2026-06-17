using System.ComponentModel.DataAnnotations;

namespace LoanEligibilitySystem.DTOs;

public class LoanApplicationRequestDto
{
    [Required(ErrorMessage = "Applicant name is required")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string ApplicantName { get; set; } = string.Empty;

    [Range(1, 120, ErrorMessage = "Age must be a valid positive number")]
    public int Age { get; set; }

    // Business rule (21-60) is checked in Service, not here
    // DTO just ensures it's a real number

    [Range(1, double.MaxValue, ErrorMessage = "Monthly income must be greater than zero")]
    public decimal MonthlyIncome { get; set; }

    [Required(ErrorMessage = "Employment type is required")]
    [RegularExpression("^(Salaried|Self-Employed)$",
        ErrorMessage = "Employment type must be 'Salaried' or 'Self-Employed'")]
    public string EmploymentType { get; set; } = string.Empty;

    [Range(0, 50, ErrorMessage = "Experience years must be between 0 and 50")]
    public double ExperienceYears { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Existing EMI cannot be negative")]
    public decimal ExistingEMI { get; set; }

    [Range(1, double.MaxValue, ErrorMessage = "Loan amount must be greater than zero")]
    public decimal LoanAmount { get; set; }

    [Range(1, 360, ErrorMessage = "Loan tenure must be between 1 and 360 months")]
    public int LoanTenure { get; set; }

    [Range(300, 900, ErrorMessage = "Credit score must be between 300 and 900")]
    public int CreditScore { get; set; }
}