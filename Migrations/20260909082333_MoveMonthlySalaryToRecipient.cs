using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareHomeApi.Migrations
{
    /// <inheritdoc />
    public partial class MoveMonthlySalaryToRecipient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "monthly_salary",
                table: "care_providers");

            migrationBuilder.AddColumn<decimal>(
                name: "monthly_salary",
                table: "care_recipients",
                type: "numeric",
                nullable: true);

            // Restore the value that was on care_providers.monthly_salary for Hafize Şen
            // (id 13 in care_recipients) before she was moved out of care_providers.
            migrationBuilder.Sql("UPDATE care_recipients SET monthly_salary = 7300.00 WHERE id = 13;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "monthly_salary",
                table: "care_recipients");

            migrationBuilder.AddColumn<decimal>(
                name: "monthly_salary",
                table: "care_providers",
                type: "numeric",
                nullable: true);
        }
    }
}
