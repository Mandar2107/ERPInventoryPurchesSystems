using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPInventoryPurchesSystems.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Approvals_Departments_DepartmentCode",
                table: "Approvals");

            migrationBuilder.DropForeignKey(
                name: "FK_Approvals_PurchaseRequisitions_PRId",
                table: "Approvals");

            migrationBuilder.AddForeignKey(
                name: "FK_Approvals_Departments_DepartmentCode",
                table: "Approvals",
                column: "DepartmentCode",
                principalTable: "Departments",
                principalColumn: "DepartmentCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Approvals_PurchaseRequisitions_PRId",
                table: "Approvals",
                column: "PRId",
                principalTable: "PurchaseRequisitions",
                principalColumn: "PRId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Approvals_Departments_DepartmentCode",
                table: "Approvals");

            migrationBuilder.DropForeignKey(
                name: "FK_Approvals_PurchaseRequisitions_PRId",
                table: "Approvals");

            migrationBuilder.AddForeignKey(
                name: "FK_Approvals_Departments_DepartmentCode",
                table: "Approvals",
                column: "DepartmentCode",
                principalTable: "Departments",
                principalColumn: "DepartmentCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Approvals_PurchaseRequisitions_PRId",
                table: "Approvals",
                column: "PRId",
                principalTable: "PurchaseRequisitions",
                principalColumn: "PRId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
