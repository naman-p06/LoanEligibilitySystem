using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanEligibilitySystem.Models;

[Table("LoanApplications")]
public class LoanApplication
{
    [Key]
    public int ApplicationId { get; set; }

    [Required]
    [MaxLength(20)]
    public string ApplicationNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string ApplicantName { get; set; } = string.Empty;

    public int Age { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal MonthlyIncome { get; set; }

    [Required]
    [MaxLength(20)]
    public string EmploymentType { get; set; } = string.Empty;

    public double ExperienceYears { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ExistingEMI { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal LoanAmount { get; set; }

    public int LoanTenure { get; set; }

    public int CreditScore { get; set; }

    [MaxLength(20)]
    public string Status { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Remarks { get; set; } = string.Empty;

    public DateTime AppliedDate { get; set; } = DateTime.UtcNow;

    
    [Column(TypeName = "decimal(18,2)")]
    public decimal CalculatedEMI { get; set; }
}