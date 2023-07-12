using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaggicVilaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaId",
                table: "VillasNumber",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 8, 24, 14, 62, DateTimeKind.Local).AddTicks(4281));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 8, 24, 14, 62, DateTimeKind.Local).AddTicks(4298));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 8, 24, 14, 62, DateTimeKind.Local).AddTicks(4300));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 8, 24, 14, 62, DateTimeKind.Local).AddTicks(4302));

            migrationBuilder.CreateIndex(
                name: "IX_VillasNumber_VillaId",
                table: "VillasNumber",
                column: "VillaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VillasNumber_Villas_VillaId",
                table: "VillasNumber",
                column: "VillaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillasNumber_Villas_VillaId",
                table: "VillasNumber");

            migrationBuilder.DropIndex(
                name: "IX_VillasNumber_VillaId",
                table: "VillasNumber");

            migrationBuilder.DropColumn(
                name: "VillaId",
                table: "VillasNumber");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 7, 55, 34, 842, DateTimeKind.Local).AddTicks(9314));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 7, 55, 34, 842, DateTimeKind.Local).AddTicks(9337));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 7, 55, 34, 842, DateTimeKind.Local).AddTicks(9340));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2023, 7, 12, 7, 55, 34, 842, DateTimeKind.Local).AddTicks(9344));
        }
    }
}
