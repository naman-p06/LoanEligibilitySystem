using LoanEligibilitySystem.DTOs;

namespace LoanEligibilitySystem.Services;

public interface ILoanEligibilityService
{
    (string status, string remarks, decimal emi) Evaluate(LoanApplicationRequestDto request);
}