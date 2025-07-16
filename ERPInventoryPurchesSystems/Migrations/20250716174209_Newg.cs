using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPInventoryPurchesSystems.Migrations
{
    /// <inheritdoc />
    public partial class Newg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestForQuotations_Vendors_VendorCode",
                table: "RequestForQuotations");

            migrationBuilder.AlterColumn<string>(
                name: "VendorCode",
                table: "RequestForQuotations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestForQuotations_Vendors_VendorCode",
                table: "RequestForQuotations",
                column: "VendorCode",
                principalTable: "Vendors",
                principalColumn: "VendorCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestForQuotations_Vendors_VendorCode",
                table: "RequestForQuotations");

            migrationBuilder.AlterColumn<string>(
                name: "VendorCode",
                table: "RequestForQuotations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestForQuotations_Vendors_VendorCode",
                table: "RequestForQuotations",
                column: "VendorCode",
                principalTable: "Vendors",
                principalColumn: "VendorCode",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
