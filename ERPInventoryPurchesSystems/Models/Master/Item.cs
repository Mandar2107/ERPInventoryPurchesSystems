using System;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.Master
{
    public class Item
    {
        [Key]
        [Required]
        public string ItemCode { get; set; }

        [Required]
        public string ItemName { get; set; }

        public string ItemDescription { get; set; }

        public string ItemCategory { get; set; }

        public string ItemType { get; set; }

        public string HSNSACCode { get; set; }

        public string UOM { get; set; }

        public string Brand { get; set; }

        public string ModelSpecification { get; set; }

        public int ReorderLevel { get; set; }

        public int MinimumStockLevel { get; set; }

        public int MaximumStockLevel { get; set; }

        public string DefaultWarehouseLocation { get; set; }

        public bool BatchTracking { get; set; }

        public bool SerialNumberTracking { get; set; }

        public decimal StandardCost { get; set; }

        public decimal LastPurchasePrice { get; set; }

        public decimal SellingPrice { get; set; }

        public decimal TaxRate { get; set; }

        public string DiscountStructure { get; set; }

        public string PreferredVendor { get; set; }

        public string VendorItemCode { get; set; }

        public int LeadTime { get; set; }

        public string PurchaseUOM { get; set; }

        public string SalesUOM { get; set; }

        public string SalesDescription { get; set; }

        public string SalesTaxGroup { get; set; }

        public string BarcodeQRCode { get; set; }

        public string MSDSComplianceDocs { get; set; }

        public string CustomAttributes { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
