using Microsoft.EntityFrameworkCore;
using LoanEligibilitySystem.Data;
using LoanEligibilitySystem.Models;

namespace LoanEligibilitySystem.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly AppDbContext _context;

    // AppDbContext is injected — same as @Autowired in Spring
    public LoanRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<LoanApplication> CreateAsync(LoanApplication application)
    {
        _context.LoanApplications.Add(application);
        await _context.SaveChangesAsync();
        return application;
    }

    public async Task<List<LoanApplication>> GetAllAsync()
    {
        return await _context.LoanApplications
            .OrderByDescending(a => a.AppliedDate)
            .ToListAsync();
    }

    public async Task<LoanApplication?> GetByIdAsync(int id)
    {
        return await _context.LoanApplications
            .FirstOrDefaultAsync(a => a.ApplicationId == id);
    }

    public async Task<LoanApplication?> GetByApplicationNumberAsync(string applicationNumber)
    {
        return await _context.LoanApplications
            .FirstOrDefaultAsync(a => a.ApplicationNumber == applicationNumber);
    }
    
    public async Task<string> GenerateApplicationNumberAsync()
    {
        var count = await _context.LoanApplications.CountAsync();
        return $"APP{1001 + count}";
    }
}