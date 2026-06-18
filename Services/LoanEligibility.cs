using LoanEligibilitySystem.DTOs;

namespace LoanEligibilitySystem.Services;

public class LoanEligibilityService : ILoanEligibilityService
{
    public (string status, string remarks, decimal emi) Evaluate(LoanApplicationRequestDto request)
    {
        // Calculate new EMI first — needed for Rule 5
        decimal newEmi = EmiCalculator.Calculate(request.LoanAmount, request.LoanTenure);

        // Rule 1 — Age check
        if (request.Age < 21 || request.Age > 60)
            return ("Rejected", $"Age {request.Age} is outside the eligible range of 21–60 years.", newEmi);

        // Rule 2 — Minimum income check
        if (request.MonthlyIncome < 25000)
            return ("Rejected", $"Monthly income ₹{request.MonthlyIncome} is below the minimum required ₹25,000.", newEmi);

        // Rule 3 — Employment experience check
        if (request.EmploymentType == "Salaried" && request.ExperienceYears < 1)
            return ("Rejected", "Salaried applicants must have at least 1 year of work experience.", newEmi);

        if (request.EmploymentType == "Self-Employed" && request.ExperienceYears < 2)
            return ("Rejected", "Self-employed applicants must have at least 2 years of business experience.", newEmi);

        // Rule 5 — EMI affordability check (do this before Rule 4)
        decimal totalEmi = request.ExistingEMI + newEmi;
        decimal maxAllowedEmi = request.MonthlyIncome * 0.5m;

        if (totalEmi > maxAllowedEmi)
            return ("Rejected",
                $"Total EMI obligation ₹{totalEmi:F2} exceeds 50% of monthly income (₹{maxAllowedEmi:F2}).",
                newEmi);

        // Rule 4 — Credit score (decides final status)
        if (request.CreditScore < 700)
            return ("Rejected", $"Credit score {request.CreditScore} is below the minimum required score of 700.", newEmi);

        if (request.CreditScore <= 799)
            return ("Under Review",
                $"Credit score {request.CreditScore} qualifies for manual review. A loan officer will contact you.",
                newEmi);

        // Credit score >= 800 — all rules passed
        return ("Approved",
            $"Congratulations! Your loan of ₹{request.LoanAmount:F2} has been approved. Monthly EMI will be ₹{newEmi:F2}.",
            newEmi);
    }
}