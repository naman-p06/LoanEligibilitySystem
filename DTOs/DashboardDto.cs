namespace LoanEligibilitySystem.DTOs;

public class DashboardDto
{
    public int TotalApplications { get; set; }
    public int Approved { get; set; }
    public int Rejected { get; set; }
    public int UnderReview { get; set; }
    public decimal TotalLoanAmountApproved { get; set; }
}