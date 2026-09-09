using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareHomeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueBillPeriodIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "CREATE UNIQUE INDEX ux_bills_type_month ON bills (utility_bill_type_id, date_trunc('month', paid_date::timestamp));");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP INDEX ux_bills_type_month;");
        }
    }
}
