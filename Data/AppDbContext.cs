using Microsoft.EntityFrameworkCore;
using LoanEligibilitySystem.Models;

namespace LoanEligibilitySystem.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<LoanApplication> LoanApplications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoanApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationId);
            entity.Property(e => e.ApplicationNumber).IsRequired().HasMaxLength(20);
            entity.Property(e => e.ApplicantName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.EmploymentType).IsRequired().HasMaxLength(20);
            entity.Property(e => e.MonthlyIncome).HasColumnType("decimal(18,2)");
            entity.Property(e => e.ExistingEMI).HasColumnType("decimal(18,2)");
            entity.Property(e => e.LoanAmount).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Remarks).HasMaxLength(500);
        });
    }
}