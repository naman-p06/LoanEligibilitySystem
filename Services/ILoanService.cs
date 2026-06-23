using LoanEligibilitySystem.DTOs;

namespace LoanEligibilitySystem.Services;

public interface ILoanService
{
    Task<LoanApplicationResponseDto> ApplyAsync(LoanApplicationRequestDto request);
    Task<List<LoanApplicationResponseDto>> GetAllAsync();
    Task<LoanApplicationResponseDto?> GetByIdAsync(int id);
    Task<LoanApplicationResponseDto?> GetByApplicationNumberAsync(string applicationNumber);
    Task<DashboardDto> GetDashboardAsync();
    Task<PagedResultDto<LoanApplicationResponseDto>> GetAllPagedAsync(int page, int pageSize, string? sortBy, string? order);
}