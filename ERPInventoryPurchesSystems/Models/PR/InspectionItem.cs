using ERPInventoryPurchesSystems.Models.Master;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.PR
{
    public class InspectionItem
    {

        [Key]
        public int InspectionItemId { get; set; }

        public int InspectionId { get; set; }
        [ForeignKey("InspectionId")]
        public QualityInspection Inspection { get; set; }

        public string ItemCode { get; set; }
        [ForeignKey("ItemCode")]
        public Item Item { get; set; }

        public int ReceivedQuantity { get; set; }

        public int InspectedQuantity { get; set; }

        public string InspectionResult { get; set; }

        public string DefectsFound { get; set; }

        public string Remarks { get; set; }
    }
}
