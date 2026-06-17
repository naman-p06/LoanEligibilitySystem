using LoanEligibilitySystem.Models;

namespace LoanEligibilitySystem.Repositories;

public interface ILoanRepository
{
    Task<LoanApplication> CreateAsync(LoanApplication application);
    Task<List<LoanApplication>> GetAllAsync();
    Task<LoanApplication?> GetByIdAsync(int id);
    Task<LoanApplication?> GetByApplicationNumberAsync(string applicationNumber);
    Task<string> GenerateApplicationNumberAsync();
}