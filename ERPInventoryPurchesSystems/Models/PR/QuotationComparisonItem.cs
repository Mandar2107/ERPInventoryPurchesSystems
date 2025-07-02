using ERPInventoryPurchesSystems.Models.Master;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.PR
{
    public class QuotationComparisonItem
    {
        [Key]
        public int ComparisonItemId { get; set; }

        public int ComparisonId { get; set; }
        [ForeignKey("ComparisonId")]
        public QuotationComparison Comparison { get; set; }

        public string VendorCode { get; set; }
        [ForeignKey("VendorCode")]
        public Vendor Vendor { get; set; }

        public string ItemCode { get; set; }
        [ForeignKey("ItemCode")]
        public Item Item { get; set; }

        public int QuotedQuantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public string DeliveryTime { get; set; }

        public int VendorRating { get; set; }

        public string Remarks { get; set; }
    }
}
