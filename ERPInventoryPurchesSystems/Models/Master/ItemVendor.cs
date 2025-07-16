using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.Master
{
    public class ItemVendor
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string ItemCode { get; set; }

        [ForeignKey("ItemCode")]
        public Item Item { get; set; }

        [Required]
        public string VendorCode { get; set; }

        [ForeignKey("VendorCode")]
        public Vendor Vendor { get; set; }

        public decimal? PurchasePrice { get; set; }
        public int? LeadTime
        {
            get; set;

        }
    }
}
