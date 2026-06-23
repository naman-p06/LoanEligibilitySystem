
using LoanEligibilitySystem.Models;
using LoanEligibilitySystem.Repositories;
using LoanEligibilitySystem.DTOs;

namespace LoanEligibilitySystem.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _repository;
    private readonly ILoanEligibilityService _eligibilityService;

    public LoanService(ILoanRepository repository, ILoanEligibilityService eligibilityService)
    {
        _repository = repository;
        _eligibilityService = eligibilityService;
    }

    public async Task<LoanApplicationResponseDto> ApplyAsync(LoanApplicationRequestDto request)
    {
        var applicationNumber = await _repository.GenerateApplicationNumberAsync();

        // Run all 5 rules
        var (status, remarks, emi) = _eligibilityService.Evaluate(request);

        var application = new LoanApplication
        {
            ApplicationNumber = applicationNumber,
            ApplicantName     = request.ApplicantName,
            Age               = request.Age,
            MonthlyIncome     = request.MonthlyIncome,
            EmploymentType    = request.EmploymentType,
            ExperienceYears   = request.ExperienceYears,
            ExistingEMI       = request.ExistingEMI,
            LoanAmount        = request.LoanAmount,
            LoanTenure        = request.LoanTenure,
            CreditScore       = request.CreditScore,
            Status            = status,
            Remarks           = remarks,
            CalculatedEMI     = emi,
            AppliedDate       = DateTime.UtcNow
        };

        var saved = await _repository.CreateAsync(application);
        return MapToResponse(saved);
    }

    public async Task<List<LoanApplicationResponseDto>> GetAllAsync()
    {
        var applications = await _repository.GetAllAsync();
        return applications.Select(MapToResponse).ToList();
    }

    public async Task<LoanApplicationResponseDto?> GetByIdAsync(int id)
    {
        var application = await _repository.GetByIdAsync(id);
        if (application == null) return null;
        return MapToResponse(application);
    }
    public async Task<LoanApplicationResponseDto?> GetByApplicationNumberAsync(string applicationNumber)
    {
        var application = await _repository.GetByApplicationNumberAsync(applicationNumber);
        if (application == null) return null;
        return MapToResponse(application);
    }
    private static LoanApplicationResponseDto MapToResponse(LoanApplication app)
    {
        return new LoanApplicationResponseDto
        {
            ApplicationNumber = app.ApplicationNumber,
            ApplicantName     = app.ApplicantName,
            LoanAmount        = app.LoanAmount,
            CalculatedEMI     = app.CalculatedEMI,
            Status            = app.Status,
            Remarks           = app.Remarks,
            AppliedDate       = app.AppliedDate
        };
    }
    public async Task<DashboardDto> GetDashboardAsync()
    {
        return await _repository.GetDashboardStatsAsync();
    }

    public async Task<PagedResultDto<LoanApplicationResponseDto>> GetAllPagedAsync(
        int page, int pageSize, string? sortBy, string? order)
    {
        var applications = await _repository.GetAllAsync();

        // Sorting
        applications = sortBy?.ToLower() switch
        {
            "loanamount"    => order == "desc"
                                ? applications.OrderByDescending(a => a.LoanAmount).ToList()
                                : applications.OrderBy(a => a.LoanAmount).ToList(),
            "applicantname" => order == "desc"
                                ? applications.OrderByDescending(a => a.ApplicantName).ToList()
                                : applications.OrderBy(a => a.ApplicantName).ToList(),
            _               => applications.OrderByDescending(a => a.AppliedDate).ToList()
        };

        var totalCount = applications.Count;
        var data = applications
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(MapToResponse)
            .ToList();

        return new PagedResultDto<LoanApplicationResponseDto>
        {
            TotalCount = totalCount,
            Page       = page,
            PageSize   = pageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
            Data       = data
        };
    }
}