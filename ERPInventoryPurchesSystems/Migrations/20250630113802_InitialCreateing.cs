using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPInventoryPurchesSystems.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentHead = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExtensionNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimaryFunction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkedModules = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalAuthorityLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentCode);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultUOM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StorageRequirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectionRequired = table.Column<bool>(type: "bit", nullable: false),
                    AssociatedGLAccounts = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnableAnalytics = table.Column<bool>(type: "bit", nullable: false),
                    ReorderPolicy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalWorkflow = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeHistory = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryCode);
                    table.ForeignKey(
                        name: "FK_Categories_Departments_DepartmentCode",
                        column: x => x.DepartmentCode,
                        principalTable: "Departments",
                        principalColumn: "DepartmentCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    VendorCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VendorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPersonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaxNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentTerms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreditLimit = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    BankAccountDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PANNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GSTVATRegistration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MSMECertificate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISOCertification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractAgreement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlacklistStatus = table.Column<bool>(type: "bit", nullable: false),
                    LastPurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalSpend = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AverageLeadTime = table.Column<int>(type: "int", nullable: false),
                    DeliveryReliabilityScore = table.Column<int>(type: "int", nullable: false),
                    QualityRating = table.Column<int>(type: "int", nullable: false),
                    VendorItemCodes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.VendorCode);
                    table.ForeignKey(
                        name: "FK_Vendors_Categories_CategoryCode",
                        column: x => x.CategoryCode,
                        principalTable: "Categories",
                        principalColumn: "CategoryCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemCategoryCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HSNSACCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UOM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelSpecification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReorderLevel = table.Column<int>(type: "int", nullable: false),
                    MinimumStockLevel = table.Column<int>(type: "int", nullable: false),
                    MaximumStockLevel = table.Column<int>(type: "int", nullable: false),
                    DefaultWarehouseLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BatchTracking = table.Column<bool>(type: "bit", nullable: false),
                    SerialNumberTracking = table.Column<bool>(type: "bit", nullable: false),
                    StandardCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LastPurchasePrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxRate = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    DiscountStructure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferredVendorCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    VendorItemCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeadTime = table.Column<int>(type: "int", nullable: false),
                    PurchaseUOM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesUOM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalesDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MSDSComplianceDocs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomAttributes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemCode);
                    table.ForeignKey(
                        name: "FK_Items_Categories_ItemCategoryCode",
                        column: x => x.ItemCategoryCode,
                        principalTable: "Categories",
                        principalColumn: "CategoryCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Vendors_PreferredVendorCode",
                        column: x => x.PreferredVendorCode,
                        principalTable: "Vendors",
                        principalColumn: "VendorCode",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VendorCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PasswordExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountLockStatus = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentCode",
                        column: x => x.DepartmentCode,
                        principalTable: "Departments",
                        principalColumn: "DepartmentCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Vendors_VendorCode",
                        column: x => x.VendorCode,
                        principalTable: "Vendors",
                        principalColumn: "VendorCode");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DepartmentCode",
                table: "Categories",
                column: "DepartmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemCategoryCode",
                table: "Items",
                column: "ItemCategoryCode");

            migrationBuilder.CreateIndex(
                name: "IX_Items_PreferredVendorCode",
                table: "Items",
                column: "PreferredVendorCode");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentCode",
                table: "Users",
                column: "DepartmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_Users_VendorCode",
                table: "Users",
                column: "VendorCode");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_CategoryCode",
                table: "Vendors",
                column: "CategoryCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vendors");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
