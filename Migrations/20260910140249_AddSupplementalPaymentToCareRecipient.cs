using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareHomeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplementalPaymentToCareRecipient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "supplemental_payment",
                table: "care_recipients",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "supplemental_payment",
                table: "care_recipients");
        }
    }
}
