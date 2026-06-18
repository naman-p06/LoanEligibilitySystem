namespace LoanEligibilitySystem.Services;

public static class EmiCalculator
{
    // Standard bank EMI formula
    // P = principal, r = monthly interest rate, n = tenure in months
    public static decimal Calculate(decimal principal, int tenureMonths, double annualInterestRate = 10.0)
    {
        double r = annualInterestRate / 12 / 100;  // monthly rate
        double n = tenureMonths;
        double p = (double)principal;

        // EMI = P × r × (1+r)^n / ((1+r)^n − 1)
        double emi = p * r * Math.Pow(1 + r, n) / (Math.Pow(1 + r, n) - 1);

        return Math.Round((decimal)emi, 2);
    }
}