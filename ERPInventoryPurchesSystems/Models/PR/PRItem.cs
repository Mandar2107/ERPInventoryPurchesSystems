using ERPInventoryPurchesSystems.Models.Master;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.PR
{
    public class PRItem
    {

    
        [Key]
        public int PRItemId { get; set; }

        public int PRId { get; set; }
        [ForeignKey("PRId")]
        public PurchesRequstiaon PurchaseRequisition { get; set; }

        public string ItemCode { get; set; }
        [ForeignKey("ItemCode")]
        public Item Item { get; set; }

        public int Quantity { get; set; }

        public DateTime RequiredDate { get; set; }

        public string Justification { get; set; }
    
}
}
