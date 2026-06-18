using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanEligibilitySystem.Migrations
{
    /// <inheritdoc />
    public partial class AddCalculatedEMI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CalculatedEMI",
                table: "LoanApplications",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CalculatedEMI",
                table: "LoanApplications");
        }
    }
}
