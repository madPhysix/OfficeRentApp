using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OfficeRentApp.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsEmpty = table.Column<bool>(type: "bit", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartOfRent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndOfRent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfficeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "Id", "Address", "BuildingName", "Floor", "ImagePath", "IsEmpty", "PricePerHour" },
                values: new object[] { 1, "44 Jafar Jabbarli street, Baku 1065", "Caspian Plaza", 8, null, true, 60m });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "Id", "Address", "BuildingName", "Floor", "ImagePath", "IsEmpty", "PricePerHour" },
                values: new object[] { 2, "44 Jafar Jabbarli street, Baku 1065", "Caspian Plaza", 15, null, true, 85m });

            migrationBuilder.InsertData(
                table: "Offices",
                columns: new[] { "Id", "Address", "BuildingName", "Floor", "ImagePath", "IsEmpty", "PricePerHour" },
                values: new object[] { 3, "44 Jafar Jabbarli street, Baku 1065", "Caspian Plaza", 3, null, true, 40m });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_OfficeId",
                table: "Rentals",
                column: "OfficeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "Offices");
        }
    }
}
