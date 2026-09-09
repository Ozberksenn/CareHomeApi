using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CareHomeApi.Migrations
{
    /// <inheritdoc />
    public partial class RestructureBillsForUtilityBills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "bills_recipient_id_fkey",
                table: "bills");

            migrationBuilder.DropColumn(
                name: "description",
                table: "bills");

            migrationBuilder.DropColumn(
                name: "is_paid",
                table: "bills");

            migrationBuilder.DropColumn(
                name: "recipient_id",
                table: "bills");

            migrationBuilder.AddColumn<int>(
                name: "utility_bill_type_id",
                table: "bills",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "utility_bill_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_utility_bill_types", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "utility_bill_types",
                columns: new[] { "id", "created_at", "name" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Su" },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Elektrik" },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Doğalgaz" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_bills_utility_bill_type_id",
                table: "bills",
                column: "utility_bill_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_bills_utility_bill_types_utility_bill_type_id",
                table: "bills",
                column: "utility_bill_type_id",
                principalTable: "utility_bill_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_bills_utility_bill_types_utility_bill_type_id",
                table: "bills");

            migrationBuilder.DropTable(
                name: "utility_bill_types");

            migrationBuilder.DropIndex(
                name: "ix_bills_utility_bill_type_id",
                table: "bills");

            migrationBuilder.DropColumn(
                name: "utility_bill_type_id",
                table: "bills");

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "bills",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_paid",
                table: "bills",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "recipient_id",
                table: "bills",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "bills_recipient_id_fkey",
                table: "bills",
                column: "recipient_id",
                principalTable: "care_recipients",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
