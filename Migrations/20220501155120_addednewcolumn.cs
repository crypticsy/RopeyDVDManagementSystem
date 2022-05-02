using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RopeyDVDManagementSystem.Migrations
{
    public partial class addednewcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Franchise",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "MembershipCategoryName",
                table: "MembershipCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DVDPoster",
                table: "DVDTitles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "DVDCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MembershipCategoryName",
                table: "MembershipCategories");

            migrationBuilder.DropColumn(
                name: "DVDPoster",
                table: "DVDTitles");

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "DVDCategories");

            migrationBuilder.AddColumn<string>(
                name: "Franchise",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
