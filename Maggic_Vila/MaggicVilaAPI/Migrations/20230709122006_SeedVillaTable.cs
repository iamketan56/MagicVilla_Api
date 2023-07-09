using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MaggicVilaAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImgUrl", "Name", "Occupency", "Sqft", "UpdatedDate", "rate" },
                values: new object[,]
                {
                    { 1, "xyz", new DateTime(2023, 7, 9, 17, 50, 6, 505, DateTimeKind.Local).AddTicks(7059), "lorem lorem", "", "Royal Villa", 5, 300, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200.0 },
                    { 2, "xyz", new DateTime(2023, 7, 9, 17, 50, 6, 505, DateTimeKind.Local).AddTicks(7077), "lorem lorem", "", "Royal Villa 1", 6, 400, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 300.0 },
                    { 3, "xyz", new DateTime(2023, 7, 9, 17, 50, 6, 505, DateTimeKind.Local).AddTicks(7080), "lorem lorem", "", "Royal Villa 2", 4, 150, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 100.0 },
                    { 4, "xyz", new DateTime(2023, 7, 9, 17, 50, 6, 505, DateTimeKind.Local).AddTicks(7082), "lorem lorem", "", "Royal Villa 3", 7, 500, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
