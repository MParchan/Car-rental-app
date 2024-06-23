using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalServer.Repository.Migrations
{
    public partial class AddingResrvationsAndDeleteBehaviorSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_CarTypes_CarTypeId",
                table: "Models");

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    RentalLocationId = table.Column<int>(type: "int", nullable: false),
                    ReturnLocationId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RentPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_Reservations_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Locations_RentalLocationId",
                        column: x => x.RentalLocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Locations_ReturnLocationId",
                        column: x => x.ReturnLocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CarId",
                table: "Reservations",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_RentalLocationId",
                table: "Reservations",
                column: "RentalLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReturnLocationId",
                table: "Reservations",
                column: "ReturnLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_CarTypes_CarTypeId",
                table: "Models",
                column: "CarTypeId",
                principalTable: "CarTypes",
                principalColumn: "CarTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Models_CarTypes_CarTypeId",
                table: "Models");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Models_CarTypes_CarTypeId",
                table: "Models",
                column: "CarTypeId",
                principalTable: "CarTypes",
                principalColumn: "CarTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
