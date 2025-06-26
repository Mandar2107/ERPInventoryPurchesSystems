using System;
using System.ComponentModel.DataAnnotations;


namespace ERPInventoryPurchesSystems.Models.Master
{
   
    public class Item
    {
        [Key]
        [Required]
        [Display(Name = "Item Code")]
        public string ItemCode { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }

        [Display(Name = "Item Description")]
        public string ItemDescription { get; set; }

        [Display(Name = "Item Category")]
        public string ItemCategory { get; set; }

        [Display(Name = "Item Type")]
        public string ItemType { get; set; }

        [Display(Name = "HSN/SAC Code")]
        public string HSNSACCode { get; set; }

        [Display(Name = "Unit of Measure")]
        public string UOM { get; set; }

        public string Brand { get; set; }

        [Display(Name = "Model/Specification")]
        public string ModelSpecification { get; set; }

        [Display(Name = "Reorder Level")]
        public int ReorderLevel { get; set; }

        [Display(Name = "Minimum Stock Level")]
        public int MinimumStockLevel { get; set; }

        [Display(Name = "Maximum Stock Level")]
        public int MaximumStockLevel { get; set; }

        [Display(Name = "Default Warehouse/Location")]
        public string DefaultWarehouseLocation { get; set; }

        [Display(Name = "Batch/Lot Tracking")]
        public bool BatchTracking { get; set; }

        [Display(Name = "Serial Number Tracking")]
        public bool SerialNumberTracking { get; set; }

        [Display(Name = "Standard Cost")]
        public decimal StandardCost { get; set; }

        [Display(Name = "Last Purchase Price")]
        public decimal LastPurchasePrice { get; set; }

        [Display(Name = "Selling Price")]
        public decimal SellingPrice { get; set; }

        [Display(Name = "Tax Rate")]
        public decimal TaxRate { get; set; }

        [Display(Name = "Discount Structure")]
        public string DiscountStructure { get; set; }

        [Display(Name = "Preferred Vendor")]
        public string PreferredVendor { get; set; }

        [Display(Name = "Vendor Item Code")]
        public string VendorItemCode { get; set; }

        [Display(Name = "Lead Time (Days)")]
        public int LeadTime { get; set; }

        [Display(Name = "Purchase UOM")]
        public string PurchaseUOM { get; set; }

        [Display(Name = "Sales UOM")]
        public string SalesUOM { get; set; }

        [Display(Name = "Sales Description")]
        public string SalesDescription { get; set; }

        [Display(Name = "Sales Tax Group")]
        public string SalesTaxGroup { get; set; }

        [Display(Name = "Barcode/QR Code")]
        public string BarcodeQRCode { get; set; }

        [Display(Name = "MSDS/Compliance Docs")]
        public string MSDSComplianceDocs { get; set; }

        [Display(Name = "Custom Attributes")]
        public string CustomAttributes { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }

        [Display(Name = "Last Modified Date")]
        public DateTime LastModifiedDate { get; set; }
    }

}
