using ERPInventoryPurchesSystems.Models.Master;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.PR
{
    public class GRNItem
    {

        [Key]
        public int GRNItemId { get; set; }

        public int GRNId { get; set; }
        [ForeignKey("GRNId")]
        public GoodsReceiptNote GRN { get; set; }

        public string ItemCode { get; set; }
        [ForeignKey("ItemCode")]
        public Item Item { get; set; }

        public int OrderedQuantity { get; set; }

        public int ReceivedQuantity { get; set; }

        public string Unit { get; set; }

        public string Condition { get; set; }

        public string Remarks { get; set; }
    }
}
