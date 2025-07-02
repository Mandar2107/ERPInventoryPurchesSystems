using ERPInventoryPurchesSystems.Models.Master;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.PR
{
    public class POItem
    {
        [Key]
        public int POItemId { get; set; }

        public int POId { get; set; }
        [ForeignKey("POId")]
        public PurchaseOrder PurchaseOrder { get; set; }

        public string ItemCode { get; set; }
        [ForeignKey("ItemCode")]
        public Item Item { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string DeliveryTerms { get; set; }
    }
}
