using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPInventoryPurchesSystems.Migrations
{
    /// <inheritdoc />
    public partial class pr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseRequisitions",
                columns: table => new
                {
                    PurchaseRequisitionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedByUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequisitions", x => x.PurchaseRequisitionID);
                    table.ForeignKey(
                        name: "FK_PurchaseRequisitions_Departments_DepartmentCode",
                        column: x => x.DepartmentCode,
                        principalTable: "Departments",
                        principalColumn: "DepartmentCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseRequisitions_Users_SubmittedByUserID",
                        column: x => x.SubmittedByUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Approvals",
                columns: table => new
                {
                    ApprovalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseRequisitionID = table.Column<int>(type: "int", nullable: false),
                    ApproverUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApprovalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approvals", x => x.ApprovalID);
                    table.ForeignKey(
                        name: "FK_Approvals_Departments_DepartmentCode",
                        column: x => x.DepartmentCode,
                        principalTable: "Departments",
                        principalColumn: "DepartmentCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Approvals_PurchaseRequisitions_PurchaseRequisitionID",
                        column: x => x.PurchaseRequisitionID,
                        principalTable: "PurchaseRequisitions",
                        principalColumn: "PurchaseRequisitionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Approvals_Users_ApproverUserID",
                        column: x => x.ApproverUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRItems",
                columns: table => new
                {
                    PRItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseRequisitionID = table.Column<int>(type: "int", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    RequiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Justification = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRItems", x => x.PRItemID);
                    table.ForeignKey(
                        name: "FK_PRItems_Items_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "Items",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PRItems_PurchaseRequisitions_PurchaseRequisitionID",
                        column: x => x.PurchaseRequisitionID,
                        principalTable: "PurchaseRequisitions",
                        principalColumn: "PurchaseRequisitionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    POId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PONumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PODate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PRId = table.Column<int>(type: "int", nullable: false),
                    VendorCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VendorContact = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.POId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Departments_DepartmentCode",
                        column: x => x.DepartmentCode,
                        principalTable: "Departments",
                        principalColumn: "DepartmentCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_PurchaseRequisitions_PRId",
                        column: x => x.PRId,
                        principalTable: "PurchaseRequisitions",
                        principalColumn: "PurchaseRequisitionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Users_RequestedByUserId",
                        column: x => x.RequestedByUserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Vendors_VendorCode",
                        column: x => x.VendorCode,
                        principalTable: "Vendors",
                        principalColumn: "VendorCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotationComparisons",
                columns: table => new
                {
                    ComparisonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRId = table.Column<int>(type: "int", nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfComparison = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuotationComparisonComparisonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationComparisons", x => x.ComparisonId);
                    table.ForeignKey(
                        name: "FK_QuotationComparisons_Departments_DepartmentCode",
                        column: x => x.DepartmentCode,
                        principalTable: "Departments",
                        principalColumn: "DepartmentCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationComparisons_PurchaseRequisitions_PRId",
                        column: x => x.PRId,
                        principalTable: "PurchaseRequisitions",
                        principalColumn: "PurchaseRequisitionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuotationComparisons_QuotationComparisons_QuotationComparisonComparisonId",
                        column: x => x.QuotationComparisonComparisonId,
                        principalTable: "QuotationComparisons",
                        principalColumn: "ComparisonId");
                    table.ForeignKey(
                        name: "FK_QuotationComparisons_Users_RequestedByUserId",
                        column: x => x.RequestedByUserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestForQuotations",
                columns: table => new
                {
                    RFQId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRId = table.Column<int>(type: "int", nullable: false),
                    VendorCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestForQuotations", x => x.RFQId);
                    table.ForeignKey(
                        name: "FK_RequestForQuotations_PurchaseRequisitions_PRId",
                        column: x => x.PRId,
                        principalTable: "PurchaseRequisitions",
                        principalColumn: "PurchaseRequisitionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestForQuotations_Vendors_VendorCode",
                        column: x => x.VendorCode,
                        principalTable: "Vendors",
                        principalColumn: "VendorCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceiptNotes",
                columns: table => new
                {
                    GRNId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GRNNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GRNDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    POId = table.Column<int>(type: "int", nullable: false),
                    VendorCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceivedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeliveryNoteNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransporterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InspectionStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerifiedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VerificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceiptNotes", x => x.GRNId);
                    table.ForeignKey(
                        name: "FK_GoodsReceiptNotes_Departments_DepartmentCode",
                        column: x => x.DepartmentCode,
                        principalTable: "Departments",
                        principalColumn: "DepartmentCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsReceiptNotes_PurchaseOrders_POId",
                        column: x => x.POId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "POId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceiptNotes_Users_ReceivedByUserId",
                        column: x => x.ReceivedByUserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceiptNotes_Users_VerifiedByUserId",
                        column: x => x.VerifiedByUserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GoodsReceiptNotes_Vendors_VendorCode",
                        column: x => x.VendorCode,
                        principalTable: "Vendors",
                        principalColumn: "VendorCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "POItems",
                columns: table => new
                {
                    POItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    POId = table.Column<int>(type: "int", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryTerms = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POItems", x => x.POItemId);
                    table.ForeignKey(
                        name: "FK_POItems_Items_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "Items",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_POItems_PurchaseOrders_POId",
                        column: x => x.POId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "POId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotationComparisonItems",
                columns: table => new
                {
                    ComparisonItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComparisonId = table.Column<int>(type: "int", nullable: false),
                    VendorCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuotedQuantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DeliveryTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorRating = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationComparisonItems", x => x.ComparisonItemId);
                    table.ForeignKey(
                        name: "FK_QuotationComparisonItems_Items_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "Items",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationComparisonItems_QuotationComparisons_ComparisonId",
                        column: x => x.ComparisonId,
                        principalTable: "QuotationComparisons",
                        principalColumn: "ComparisonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationComparisonItems_Vendors_VendorCode",
                        column: x => x.VendorCode,
                        principalTable: "Vendors",
                        principalColumn: "VendorCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RFQItems",
                columns: table => new
                {
                    RFQItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RFQId = table.Column<int>(type: "int", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuotationAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TermsAndConditions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RFQItems", x => x.RFQItemId);
                    table.ForeignKey(
                        name: "FK_RFQItems_Items_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "Items",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RFQItems_RequestForQuotations_RFQId",
                        column: x => x.RFQId,
                        principalTable: "RequestForQuotations",
                        principalColumn: "RFQId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GRNItems",
                columns: table => new
                {
                    GRNItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GRNId = table.Column<int>(type: "int", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderedQuantity = table.Column<int>(type: "int", nullable: false),
                    ReceivedQuantity = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRNItems", x => x.GRNItemId);
                    table.ForeignKey(
                        name: "FK_GRNItems_GoodsReceiptNotes_GRNId",
                        column: x => x.GRNId,
                        principalTable: "GoodsReceiptNotes",
                        principalColumn: "GRNId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GRNItems_Items_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "Items",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VendorCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    POId = table.Column<int>(type: "int", nullable: false),
                    GRNId = table.Column<int>(type: "int", nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalInvoiceAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentTerms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Departments_DepartmentCode",
                        column: x => x.DepartmentCode,
                        principalTable: "Departments",
                        principalColumn: "DepartmentCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_GoodsReceiptNotes_GRNId",
                        column: x => x.GRNId,
                        principalTable: "GoodsReceiptNotes",
                        principalColumn: "GRNId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_PurchaseOrders_POId",
                        column: x => x.POId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "POId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Users_ProcessedByUserId",
                        column: x => x.ProcessedByUserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Vendors_VendorCode",
                        column: x => x.VendorCode,
                        principalTable: "Vendors",
                        principalColumn: "VendorCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QualityInspections",
                columns: table => new
                {
                    InspectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GRNId = table.Column<int>(type: "int", nullable: false),
                    InspectionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InspectedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InspectionMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceStandards = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectiveActionRequired = table.Column<bool>(type: "bit", nullable: false),
                    ActionTakenByUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FollowUpDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualityInspections", x => x.InspectionId);
                    table.ForeignKey(
                        name: "FK_QualityInspections_Departments_DepartmentCode",
                        column: x => x.DepartmentCode,
                        principalTable: "Departments",
                        principalColumn: "DepartmentCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QualityInspections_GoodsReceiptNotes_GRNId",
                        column: x => x.GRNId,
                        principalTable: "GoodsReceiptNotes",
                        principalColumn: "GRNId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QualityInspections_Users_ActionTakenByUserId",
                        column: x => x.ActionTakenByUserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QualityInspections_Users_InspectedByUserId",
                        column: x => x.InspectedByUserId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItems",
                columns: table => new
                {
                    InvoiceItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<int>(type: "int", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    POQuantity = table.Column<int>(type: "int", nullable: false),
                    GRNQuantity = table.Column<int>(type: "int", nullable: false),
                    InvoicedQuantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MatchStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItems", x => x.InvoiceItemId);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItems_Items_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "Items",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InspectionItems",
                columns: table => new
                {
                    InspectionItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InspectionId = table.Column<int>(type: "int", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceivedQuantity = table.Column<int>(type: "int", nullable: false),
                    InspectedQuantity = table.Column<int>(type: "int", nullable: false),
                    InspectionResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefectsFound = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionItems", x => x.InspectionItemId);
                    table.ForeignKey(
                        name: "FK_InspectionItems_Items_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "Items",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InspectionItems_QualityInspections_InspectionId",
                        column: x => x.InspectionId,
                        principalTable: "QualityInspections",
                        principalColumn: "InspectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Approvals_ApproverUserID",
                table: "Approvals",
                column: "ApproverUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Approvals_DepartmentCode",
                table: "Approvals",
                column: "DepartmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_Approvals_PurchaseRequisitionID",
                table: "Approvals",
                column: "PurchaseRequisitionID");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptNotes_DepartmentCode",
                table: "GoodsReceiptNotes",
                column: "DepartmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptNotes_POId",
                table: "GoodsReceiptNotes",
                column: "POId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptNotes_ReceivedByUserId",
                table: "GoodsReceiptNotes",
                column: "ReceivedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptNotes_VendorCode",
                table: "GoodsReceiptNotes",
                column: "VendorCode");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptNotes_VerifiedByUserId",
                table: "GoodsReceiptNotes",
                column: "VerifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GRNItems_GRNId",
                table: "GRNItems",
                column: "GRNId");

            migrationBuilder.CreateIndex(
                name: "IX_GRNItems_ItemCode",
                table: "GRNItems",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionItems_InspectionId",
                table: "InspectionItems",
                column: "InspectionId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionItems_ItemCode",
                table: "InspectionItems",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_InvoiceId",
                table: "InvoiceItems",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_ItemCode",
                table: "InvoiceItems",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_DepartmentCode",
                table: "Invoices",
                column: "DepartmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_GRNId",
                table: "Invoices",
                column: "GRNId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_POId",
                table: "Invoices",
                column: "POId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ProcessedByUserId",
                table: "Invoices",
                column: "ProcessedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_VendorCode",
                table: "Invoices",
                column: "VendorCode");

            migrationBuilder.CreateIndex(
                name: "IX_POItems_ItemCode",
                table: "POItems",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_POItems_POId",
                table: "POItems",
                column: "POId");

            migrationBuilder.CreateIndex(
                name: "IX_PRItems_ItemCode",
                table: "PRItems",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_PRItems_PurchaseRequisitionID",
                table: "PRItems",
                column: "PurchaseRequisitionID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_DepartmentCode",
                table: "PurchaseOrders",
                column: "DepartmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_PRId",
                table: "PurchaseOrders",
                column: "PRId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_RequestedByUserId",
                table: "PurchaseOrders",
                column: "RequestedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_VendorCode",
                table: "PurchaseOrders",
                column: "VendorCode");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequisitions_DepartmentCode",
                table: "PurchaseRequisitions",
                column: "DepartmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseRequisitions_SubmittedByUserID",
                table: "PurchaseRequisitions",
                column: "SubmittedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_QualityInspections_ActionTakenByUserId",
                table: "QualityInspections",
                column: "ActionTakenByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QualityInspections_DepartmentCode",
                table: "QualityInspections",
                column: "DepartmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_QualityInspections_GRNId",
                table: "QualityInspections",
                column: "GRNId");

            migrationBuilder.CreateIndex(
                name: "IX_QualityInspections_InspectedByUserId",
                table: "QualityInspections",
                column: "InspectedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationComparisonItems_ComparisonId",
                table: "QuotationComparisonItems",
                column: "ComparisonId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationComparisonItems_ItemCode",
                table: "QuotationComparisonItems",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationComparisonItems_VendorCode",
                table: "QuotationComparisonItems",
                column: "VendorCode");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationComparisons_DepartmentCode",
                table: "QuotationComparisons",
                column: "DepartmentCode");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationComparisons_PRId",
                table: "QuotationComparisons",
                column: "PRId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationComparisons_QuotationComparisonComparisonId",
                table: "QuotationComparisons",
                column: "QuotationComparisonComparisonId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationComparisons_RequestedByUserId",
                table: "QuotationComparisons",
                column: "RequestedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForQuotations_PRId",
                table: "RequestForQuotations",
                column: "PRId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestForQuotations_VendorCode",
                table: "RequestForQuotations",
                column: "VendorCode");

            migrationBuilder.CreateIndex(
                name: "IX_RFQItems_ItemCode",
                table: "RFQItems",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_RFQItems_RFQId",
                table: "RFQItems",
                column: "RFQId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Approvals");

            migrationBuilder.DropTable(
                name: "GRNItems");

            migrationBuilder.DropTable(
                name: "InspectionItems");

            migrationBuilder.DropTable(
                name: "InvoiceItems");

            migrationBuilder.DropTable(
                name: "POItems");

            migrationBuilder.DropTable(
                name: "PRItems");

            migrationBuilder.DropTable(
                name: "QuotationComparisonItems");

            migrationBuilder.DropTable(
                name: "RFQItems");

            migrationBuilder.DropTable(
                name: "QualityInspections");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "QuotationComparisons");

            migrationBuilder.DropTable(
                name: "RequestForQuotations");

            migrationBuilder.DropTable(
                name: "GoodsReceiptNotes");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "PurchaseRequisitions");
        }
    }
}
