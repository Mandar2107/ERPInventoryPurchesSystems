using ERPInventoryPurchesSystems.Models.Master;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.PR
{
    public class InvoiceItem
    {
        [Key]
        public int InvoiceItemId { get; set; }

        public int InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; }

        public string ItemCode { get; set; }
        [ForeignKey("ItemCode")]
        public Item Item { get; set; }

        public int POQuantity { get; set; }

        public int GRNQuantity { get; set; }

        public int InvoicedQuantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalAmount { get; set; }

        public string MatchStatus { get; set; }
    }
}
