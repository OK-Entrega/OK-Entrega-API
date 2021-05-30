using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class Algunsnovoscampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPriceNFE",
                table: "Orders",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "ToCNPJ",
                table: "Orders",
                newName: "ReceiverName");

            migrationBuilder.RenameColumn(
                name: "ReceiverStateRegistration",
                table: "Orders",
                newName: "ReceiverDocument");

            migrationBuilder.RenameColumn(
                name: "ReceiverCompanyName",
                table: "Orders",
                newName: "NumericCode");

            migrationBuilder.RenameColumn(
                name: "IssuerStateRegistration",
                table: "Orders",
                newName: "NatureOperation");

            migrationBuilder.RenameColumn(
                name: "IssuerCompanyName",
                table: "Orders",
                newName: "CompanyUF");

            migrationBuilder.RenameColumn(
                name: "FromCNPJ",
                table: "Orders",
                newName: "CarrierCNPJ");

            migrationBuilder.AddColumn<string>(
                name: "CodeEmail",
                table: "Shippers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CFOP",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DispatchedAt",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ReceiverType",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalValue",
                table: "Orders",
                type: "DECIMAL",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LatitudeDeliverer",
                table: "Occurrences",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudeDeliverer",
                table: "Occurrences",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LatitudeDeliverer",
                table: "FinishedOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudeDeliverer",
                table: "FinishedOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CodeCellphoneNumber",
                table: "Deliverers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerifyingCode",
                table: "Deliverers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeEmail",
                table: "Shippers");

            migrationBuilder.DropColumn(
                name: "CFOP",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DispatchedAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ReceiverType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalValue",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LatitudeDeliverer",
                table: "Occurrences");

            migrationBuilder.DropColumn(
                name: "LongitudeDeliverer",
                table: "Occurrences");

            migrationBuilder.DropColumn(
                name: "LatitudeDeliverer",
                table: "FinishedOrders");

            migrationBuilder.DropColumn(
                name: "LongitudeDeliverer",
                table: "FinishedOrders");

            migrationBuilder.DropColumn(
                name: "CodeCellphoneNumber",
                table: "Deliverers");

            migrationBuilder.DropColumn(
                name: "VerifyingCode",
                table: "Deliverers");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Orders",
                newName: "TotalPriceNFE");

            migrationBuilder.RenameColumn(
                name: "ReceiverName",
                table: "Orders",
                newName: "ToCNPJ");

            migrationBuilder.RenameColumn(
                name: "ReceiverDocument",
                table: "Orders",
                newName: "ReceiverStateRegistration");

            migrationBuilder.RenameColumn(
                name: "NumericCode",
                table: "Orders",
                newName: "ReceiverCompanyName");

            migrationBuilder.RenameColumn(
                name: "NatureOperation",
                table: "Orders",
                newName: "IssuerStateRegistration");

            migrationBuilder.RenameColumn(
                name: "CompanyUF",
                table: "Orders",
                newName: "IssuerCompanyName");

            migrationBuilder.RenameColumn(
                name: "CarrierCNPJ",
                table: "Orders",
                newName: "FromCNPJ");
        }
    }
}
