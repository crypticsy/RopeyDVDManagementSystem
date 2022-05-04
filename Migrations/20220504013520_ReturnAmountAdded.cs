using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RopeyDVDManagementSystem.Migrations
{
    public partial class ReturnAmountAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ReturnAmount",
                table: "Loans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnAmount",
                table: "Loans");
        }
    }
}
