using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class VouchertableandVehicleTypeVehiclePlateinOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlVoucher",
                table: "FinishedOrders");

            migrationBuilder.AddColumn<string>(
                name: "VehiclePlate",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleType",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishedAt",
                table: "FinishedOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HasData = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HasSignature = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HasNumberAndSeries = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberAndSeriesIsCorrect = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataIsCorrect = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinishOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Voucher_FinishedOrders_FinishOrderId",
                        column: x => x.FinishOrderId,
                        principalTable: "FinishedOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_FinishOrderId",
                table: "Voucher",
                column: "FinishOrderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropColumn(
                name: "VehiclePlate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "VehicleType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FinishedAt",
                table: "FinishedOrders");

            migrationBuilder.AddColumn<string>(
                name: "UrlVoucher",
                table: "FinishedOrders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
