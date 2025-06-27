using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPInventoryPurchesSystems.Models.Master
{
    public class Category
    {
        [Key]
        [Required]
        public string CategoryCode { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string CategoryType { get; set; }

        public string? ParentCategory { get; set; }

        [Required]
        public string Status { get; set; }

        public string BusinessUnit { get; set; }

        public string DepartmentCode { get; set; }

        [ForeignKey("DepartmentCode")]
        public Department Department { get; set; }

        public string UsageType { get; set; }

        public string TaxGroup { get; set; }

        public string DefaultUOM { get; set; }

        public string StorageRequirements { get; set; }

        public bool InspectionRequired { get; set; }

        public ICollection<Item> Items { get; set; }

        public ICollection<Vendor> Vendors { get; set; }


        public bool EnableAnalytics { get; set; }

        public string ReorderPolicy { get; set; }

        public bool ApprovalWorkflow { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public string ChangeHistory { get; set; }
    }
}
