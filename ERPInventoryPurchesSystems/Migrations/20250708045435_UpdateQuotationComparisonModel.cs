using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPInventoryPurchesSystems.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuotationComparisonModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuotationComparisons_QuotationComparisons_QuotationComparisonComparisonId",
                table: "QuotationComparisons");

            migrationBuilder.DropIndex(
                name: "IX_QuotationComparisons_QuotationComparisonComparisonId",
                table: "QuotationComparisons");

            migrationBuilder.DropColumn(
                name: "QuotationComparisonComparisonId",
                table: "QuotationComparisons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuotationComparisonComparisonId",
                table: "QuotationComparisons",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuotationComparisons_QuotationComparisonComparisonId",
                table: "QuotationComparisons",
                column: "QuotationComparisonComparisonId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuotationComparisons_QuotationComparisons_QuotationComparisonComparisonId",
                table: "QuotationComparisons",
                column: "QuotationComparisonComparisonId",
                principalTable: "QuotationComparisons",
                principalColumn: "ComparisonId");
        }
    }
}
