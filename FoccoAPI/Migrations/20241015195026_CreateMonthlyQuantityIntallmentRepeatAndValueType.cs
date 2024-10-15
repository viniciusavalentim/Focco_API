using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoccoAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateMonthlyQuantityIntallmentRepeatAndValueType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonthlyQuantityIntallmentRepeat",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ValueType",
                table: "Transactions",
                type: "nvarchar(1)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonthlyQuantityIntallmentRepeat",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ValueType",
                table: "Transactions");
        }
    }
}
