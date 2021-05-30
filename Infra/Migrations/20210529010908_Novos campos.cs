using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class Novoscampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverType",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ReceiverEmail",
                table: "Orders",
                newName: "XMLPath");

            migrationBuilder.RenameColumn(
                name: "ReceiverDocument",
                table: "Orders",
                newName: "VerifyingDigit");

            migrationBuilder.RenameColumn(
                name: "CompanyUF",
                table: "Orders",
                newName: "UFIssuerCode");

            migrationBuilder.AddColumn<string>(
                name: "DestinationAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationCEP",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationCity",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationComplement",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationDistrict",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationNumber",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationUF",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuerAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuerCEP",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuerCity",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuerComplement",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuerDistrict",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuerNumber",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuerUF",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiverCNPJ",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlsEvidences",
                table: "Occurrences",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinationAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DestinationCEP",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DestinationCity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DestinationComplement",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DestinationDistrict",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DestinationNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DestinationUF",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IssuerAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IssuerCEP",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IssuerCity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IssuerComplement",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IssuerDistrict",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IssuerNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IssuerUF",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ReceiverCNPJ",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UrlsEvidences",
                table: "Occurrences");

            migrationBuilder.RenameColumn(
                name: "XMLPath",
                table: "Orders",
                newName: "ReceiverEmail");

            migrationBuilder.RenameColumn(
                name: "VerifyingDigit",
                table: "Orders",
                newName: "ReceiverDocument");

            migrationBuilder.RenameColumn(
                name: "UFIssuerCode",
                table: "Orders",
                newName: "CompanyUF");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverType",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
