using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class Vouchers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Voucher_FinishedOrders_FinishOrderId",
                table: "Voucher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Voucher",
                table: "Voucher");

            migrationBuilder.RenameTable(
                name: "Voucher",
                newName: "Vouchers");

            migrationBuilder.RenameIndex(
                name: "IX_Voucher_FinishOrderId",
                table: "Vouchers",
                newName: "IX_Vouchers_FinishOrderId");

            migrationBuilder.AlterColumn<string>(
                name: "Series",
                table: "Orders",
                type: "VARCHAR(3)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vouchers",
                table: "Vouchers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vouchers_FinishedOrders_FinishOrderId",
                table: "Vouchers",
                column: "FinishOrderId",
                principalTable: "FinishedOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vouchers_FinishedOrders_FinishOrderId",
                table: "Vouchers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vouchers",
                table: "Vouchers");

            migrationBuilder.RenameTable(
                name: "Vouchers",
                newName: "Voucher");

            migrationBuilder.RenameIndex(
                name: "IX_Vouchers_FinishOrderId",
                table: "Voucher",
                newName: "IX_Voucher_FinishOrderId");

            migrationBuilder.AlterColumn<string>(
                name: "Series",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(3)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Voucher",
                table: "Voucher",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Voucher_FinishedOrders_FinishOrderId",
                table: "Voucher",
                column: "FinishOrderId",
                principalTable: "FinishedOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
