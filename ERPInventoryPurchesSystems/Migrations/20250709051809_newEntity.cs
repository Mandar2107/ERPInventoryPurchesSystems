using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPInventoryPurchesSystems.Migrations
{
    public partial class newEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "QuotationComparisonItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "QuotationComparisonItems");
        }
    }
}
