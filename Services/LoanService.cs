using LoanEligibilitySystem.DTOs;
using LoanEligibilitySystem.Models;
using LoanEligibilitySystem.Repositories;

namespace LoanEligibilitySystem.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _repository;

    public LoanService(ILoanRepository repository)
    {
        _repository = repository;
    }

    public async Task<LoanApplicationResponseDto> ApplyAsync(LoanApplicationRequestDto request)
    {
        var applicationNumber = await _repository.GenerateApplicationNumberAsync();

        // Business rules go here on Day 3
        // For now we just save with a placeholder status
        var application = new LoanApplication
        {
            ApplicationNumber = applicationNumber,
            ApplicantName    = request.ApplicantName,
            Age              = request.Age,
            MonthlyIncome    = request.MonthlyIncome,
            EmploymentType   = request.EmploymentType,
            ExperienceYears  = request.ExperienceYears,
            ExistingEMI      = request.ExistingEMI,
            LoanAmount       = request.LoanAmount,
            LoanTenure       = request.LoanTenure,
            CreditScore      = request.CreditScore,
            Status           = "Pending",   // will be replaced on Day 3
            Remarks          = "Evaluation pending.",
            AppliedDate      = DateTime.UtcNow
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

    // Private mapper — same idea as MapStruct in Java but manual
    private static LoanApplicationResponseDto MapToResponse(LoanApplication app)
    {
        return new LoanApplicationResponseDto
        {
            ApplicationNumber = app.ApplicationNumber,
            ApplicantName     = app.ApplicantName,
            LoanAmount        = app.LoanAmount,
            Status            = app.Status,
            Remarks           = app.Remarks,
            AppliedDate       = app.AppliedDate
        };
    }
}