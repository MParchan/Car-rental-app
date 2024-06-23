using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalServer.Repository.Migrations
{
    public partial class AddingVersionToCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "Cars");
        }
    }
}
