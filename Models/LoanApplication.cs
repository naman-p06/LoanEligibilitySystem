namespace LoanEligibilitySystem.Models;

public class LoanApplication
{
    public int ApplicationId{get;set;}
    public string ApplicationNumber {get;set;}=String.Empty;
    public string ApplicantName{get;set;}=String.Empty;
    public int Age{get;set;}
    public decimal MonthlyIncome{get;set;}
    public string EmploymentType{get;set;}
    public double ExperienceYears {get;set;}
    public decimal ExistingEMI { get; set; }
    public decimal LoanAmount { get; set; }
    public int LoanTenure { get; set; }   // in months
    public int CreditScore { get; set; }
    public string Status { get; set; } = string.Empty;  // Approved / Rejected / Under Review
    public string Remarks { get; set; } = string.Empty;

    public DateTime AppliedDate {get;set;}=DateTime.UtcNow;
}