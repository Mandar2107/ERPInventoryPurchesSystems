using ERPInventoryPurchesSystems.Models.Master;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.PR
{
    public class QualityInspection
    {
        [Key]
        public int InspectionId { get; set; }

        public int GRNId { get; set; }
        [ForeignKey("GRNId")]
        public GoodsReceiptNote GRN { get; set; }

        public DateTime InspectionDate { get; set; }

        public string InspectedByUserId { get; set; }
        [ForeignKey("InspectedByUserId")]
        public User InspectedBy { get; set; }

        public string DepartmentCode { get; set; }
        [ForeignKey("DepartmentCode")]
        public Department Department { get; set; }

        public ICollection<InspectionItem> Items { get; set; }

        public string InspectionMethod { get; set; }

        public string ReferenceStandards { get; set; }

        public bool CorrectiveActionRequired { get; set; }

        public string ActionTakenByUserId { get; set; }
        [ForeignKey("ActionTakenByUserId")]
        public User ActionTakenBy { get; set; }

        public DateTime FollowUpDate { get; set; }
    }
}
