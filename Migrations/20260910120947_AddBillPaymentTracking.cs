using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareHomeApi.Migrations
{
    /// <inheritdoc />
    public partial class AddBillPaymentTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "paid_date",
                table: "bills",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<DateOnly>(
                name: "bill_date",
                table: "bills",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<bool>(
                name: "is_paid",
                table: "bills",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            // Existing bills were recorded once they were paid, so treat their old
            // paid_date as both the bill's arrival date and its payment date.
            migrationBuilder.Sql("UPDATE bills SET bill_date = paid_date, is_paid = true;");

            migrationBuilder.Sql("DROP INDEX IF EXISTS ux_bills_type_month;");
            migrationBuilder.Sql(
                "CREATE UNIQUE INDEX ux_bills_type_month ON bills (utility_bill_type_id, date_trunc('month', bill_date::timestamp));");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP INDEX IF EXISTS ux_bills_type_month;");

            migrationBuilder.Sql("UPDATE bills SET paid_date = bill_date WHERE paid_date IS NULL;");

            migrationBuilder.DropColumn(
                name: "bill_date",
                table: "bills");

            migrationBuilder.DropColumn(
                name: "is_paid",
                table: "bills");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "paid_date",
                table: "bills",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.Sql(
                "CREATE UNIQUE INDEX ux_bills_type_month ON bills (utility_bill_type_id, date_trunc('month', paid_date::timestamp));");
        }
    }
}
