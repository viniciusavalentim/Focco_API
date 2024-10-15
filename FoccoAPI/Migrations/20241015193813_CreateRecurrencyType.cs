using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoccoAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateRecurrencyType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecurrenceType",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecurrenceType",
                table: "Transactions");
        }
    }
}
