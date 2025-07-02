using ERPInventoryPurchesSystems.Models.Master;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.PR
{
    public class RFQItem
    {
        [Key]
        public int RFQItemId { get; set; }

        public int RFQId { get; set; }
        [ForeignKey("RFQId")]
        public RequestForQuotation RFQ { get; set; }

        public string ItemCode { get; set; }
        [ForeignKey("ItemCode")]
        public Item Item { get; set; }

        public int Quantity { get; set; }

        public DateTime DeliveryDate { get; set; }

        public decimal QuotationAmount { get; set; }

        public string TermsAndConditions { get; set; }
    }
}
