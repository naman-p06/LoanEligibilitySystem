using LoanEligibilitySystem.DTOs;

namespace LoanEligibilitySystem.Services;

public interface ILoanService
{
    Task<LoanApplicationResponseDto> ApplyAsync(LoanApplicationRequestDto request);
    Task<List<LoanApplicationResponseDto>> GetAllAsync();
    Task<LoanApplicationResponseDto?> GetByIdAsync(int id);
}