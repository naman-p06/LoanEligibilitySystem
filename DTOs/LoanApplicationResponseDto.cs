namespace LoanEligibilitySystem.DTOs;

public class LoanApplicationResponseDto
{
    public string ApplicationNumber { get; set; } = string.Empty;
    public string ApplicantName { get; set; } = string.Empty;
    public decimal LoanAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Remarks { get; set; } = string.Empty;
    public DateTime AppliedDate { get; set; }
}