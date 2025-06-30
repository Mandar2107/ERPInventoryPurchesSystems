using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPInventoryPurchesSystems.Models.Master
{
    public class Category
    {
        [Key]
        [Required(ErrorMessage = "Category code is required.")]
        public string CategoryCode { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Category type is required.")]
        public string CategoryType { get; set; }

        [Required(ErrorMessage = "Status is required.")]
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

        public string AssociatedGLAccounts { get; set; }

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
