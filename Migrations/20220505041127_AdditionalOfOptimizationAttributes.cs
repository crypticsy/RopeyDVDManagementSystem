using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RopeyDVDManagementSystem.Migrations
{
    public partial class AdditionalOfOptimizationAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ReturnAmount",
                table: "Loans",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnLoan",
                table: "DVDCopies",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnAmount",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "IsOnLoan",
                table: "DVDCopies");
        }
    }
}
