using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalServer.Repository.Migrations
{
    public partial class AddingImageUrlToModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Models",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Models");
        }
    }
}
