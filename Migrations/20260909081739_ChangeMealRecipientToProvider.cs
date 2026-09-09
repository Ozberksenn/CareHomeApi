using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareHomeApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMealRecipientToProvider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_meals_care_recipients_recipient_id",
                table: "meals");

            migrationBuilder.RenameColumn(
                name: "recipient_id",
                table: "meals",
                newName: "provider_id");

            migrationBuilder.RenameIndex(
                name: "ix_meals_recipient_id",
                table: "meals",
                newName: "ix_meals_provider_id");

            // The renamed column still holds old care_recipients ids; remap them to the
            // care_providers rows the same people were re-created under (see conversation
            // that led to this migration: these people were care staff, not care recipients).
            migrationBuilder.Sql("UPDATE meals SET provider_id = 5 WHERE provider_id = 4;");
            migrationBuilder.Sql("UPDATE meals SET provider_id = 7 WHERE provider_id = 8;");

            migrationBuilder.AddForeignKey(
                name: "fk_meals_care_providers_provider_id",
                table: "meals",
                column: "provider_id",
                principalTable: "care_providers",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_meals_care_providers_provider_id",
                table: "meals");

            migrationBuilder.RenameColumn(
                name: "provider_id",
                table: "meals",
                newName: "recipient_id");

            migrationBuilder.RenameIndex(
                name: "ix_meals_provider_id",
                table: "meals",
                newName: "ix_meals_recipient_id");

            migrationBuilder.AddForeignKey(
                name: "fk_meals_care_recipients_recipient_id",
                table: "meals",
                column: "recipient_id",
                principalTable: "care_recipients",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
