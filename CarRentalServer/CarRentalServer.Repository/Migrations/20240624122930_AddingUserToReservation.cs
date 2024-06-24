using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalServer.Repository.Migrations
{
    public partial class AddingUserToReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Reservations");
        }
    }
}
