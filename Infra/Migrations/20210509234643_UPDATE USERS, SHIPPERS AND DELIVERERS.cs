using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class UPDATEUSERSSHIPPERSANDDELIVERERS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinishedOrders_Users_DelivererId",
                table: "FinishedOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Occurrences_Users_DelivererId",
                table: "Occurrences");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipperCompanies_Users_ShipperId",
                table: "ShipperCompanies");

            migrationBuilder.DropColumn(
                name: "CellphoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "Deliverers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CellphoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliverers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliverers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shippers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shippers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shippers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliverers_UserId",
                table: "Deliverers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shippers_UserId",
                table: "Shippers",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FinishedOrders_Deliverers_DelivererId",
                table: "FinishedOrders",
                column: "DelivererId",
                principalTable: "Deliverers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Occurrences_Deliverers_DelivererId",
                table: "Occurrences",
                column: "DelivererId",
                principalTable: "Deliverers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipperCompanies_Shippers_ShipperId",
                table: "ShipperCompanies",
                column: "ShipperId",
                principalTable: "Shippers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinishedOrders_Deliverers_DelivererId",
                table: "FinishedOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Occurrences_Deliverers_DelivererId",
                table: "Occurrences");

            migrationBuilder.DropForeignKey(
                name: "FK_ShipperCompanies_Shippers_ShipperId",
                table: "ShipperCompanies");

            migrationBuilder.DropTable(
                name: "Deliverers");

            migrationBuilder.DropTable(
                name: "Shippers");

            migrationBuilder.AddColumn<string>(
                name: "CellphoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FinishedOrders_Users_DelivererId",
                table: "FinishedOrders",
                column: "DelivererId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Occurrences_Users_DelivererId",
                table: "Occurrences",
                column: "DelivererId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShipperCompanies_Users_ShipperId",
                table: "ShipperCompanies",
                column: "ShipperId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
