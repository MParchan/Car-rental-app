using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalServer.Repository.Migrations
{
    public partial class ChangingLocationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "City",
                table: "Locations",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Locations",
                newName: "City");
        }
    }
}
