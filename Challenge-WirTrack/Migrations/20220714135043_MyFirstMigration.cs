using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Challenge_WirTrack.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Travels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    VehicleID = table.Column<int>(type: "int", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Travels_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Travels_Vehicles_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "IsDeleted", "LastModified", "Name" },
                values: new object[,]
                {
                    { 1, false, new DateTime(2022, 7, 14, 10, 50, 43, 141, DateTimeKind.Local).AddTicks(6378), "Buenos Aires" },
                    { 2, false, new DateTime(2022, 7, 14, 10, 50, 43, 142, DateTimeKind.Local).AddTicks(4603), "Mar del Plata" },
                    { 3, false, new DateTime(2022, 7, 14, 10, 50, 43, 142, DateTimeKind.Local).AddTicks(4615), "La Plata" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Brand", "IsDeleted", "LastModified", "Patent", "Type" },
                values: new object[,]
                {
                    { 1, "Toyota", false, new DateTime(2022, 7, 14, 10, 50, 43, 143, DateTimeKind.Local).AddTicks(7006), "AAA000", "Car" },
                    { 2, "Honda", false, new DateTime(2022, 7, 14, 10, 50, 43, 143, DateTimeKind.Local).AddTicks(7973), "AAA001", "Truck" },
                    { 3, "Scannia", false, new DateTime(2022, 7, 14, 10, 50, 43, 143, DateTimeKind.Local).AddTicks(7978), "AAA003", "Truck" }
                });

            migrationBuilder.InsertData(
                table: "Travels",
                columns: new[] { "Id", "CityID", "Date", "IsDeleted", "LastModified", "VehicleID" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 7, 14, 0, 0, 0, 0, DateTimeKind.Local), false, new DateTime(2022, 7, 14, 10, 50, 43, 144, DateTimeKind.Local).AddTicks(2004), 1 },
                    { 2, 2, new DateTime(2022, 7, 15, 0, 0, 0, 0, DateTimeKind.Local), false, new DateTime(2022, 7, 14, 10, 50, 43, 144, DateTimeKind.Local).AddTicks(3076), 1 },
                    { 3, 3, new DateTime(2022, 7, 16, 0, 0, 0, 0, DateTimeKind.Local), false, new DateTime(2022, 7, 14, 10, 50, 43, 144, DateTimeKind.Local).AddTicks(3156), 1 },
                    { 4, 1, new DateTime(2022, 7, 14, 0, 0, 0, 0, DateTimeKind.Local), false, new DateTime(2022, 7, 14, 10, 50, 43, 144, DateTimeKind.Local).AddTicks(3162), 2 },
                    { 5, 2, new DateTime(2022, 7, 15, 0, 0, 0, 0, DateTimeKind.Local), false, new DateTime(2022, 7, 14, 10, 50, 43, 144, DateTimeKind.Local).AddTicks(3166), 2 },
                    { 6, 3, new DateTime(2022, 7, 16, 0, 0, 0, 0, DateTimeKind.Local), false, new DateTime(2022, 7, 14, 10, 50, 43, 144, DateTimeKind.Local).AddTicks(3169), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Travels_CityID",
                table: "Travels",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_VehicleID",
                table: "Travels",
                column: "VehicleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Travels");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
