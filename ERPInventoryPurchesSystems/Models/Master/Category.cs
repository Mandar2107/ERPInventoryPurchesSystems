using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.Master
{

    using System;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        [Key]
        [Required]
        [Display(Name = "Category Code")]
        public string CategoryCode { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Required]
        [Display(Name = "Category Type")]
        public string CategoryType { get; set; } // Item, Vendor, Service, Expense, etc.

        [Display(Name = "Parent Category")]
        public string? ParentCategory { get; set; }

        [Required]
        public string Status { get; set; } // Active / Inactive

        [Display(Name = "Business Unit")]
        public string BusinessUnit { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; } // Linked department

        [Display(Name = "Usage Type")]
        public string UsageType { get; set; } // Internal Use, External Sale, Both

        [Display(Name = "Tax Group")]
        public string TaxGroup { get; set; }

        [Display(Name = "Default UOM")]
        public string DefaultUOM { get; set; }

        [Display(Name = "Storage Requirements")]
        public string StorageRequirements { get; set; }

        [Display(Name = "Inspection Required")]
        public bool InspectionRequired { get; set; }

        [Display(Name = "Associated Items")]
        public string AssociatedItems { get; set; }

        [Display(Name = "Associated Vendors")]
        public string AssociatedVendors { get; set; }

        [Display(Name = "Associated GL Accounts")]
        public string AssociatedGLAccounts { get; set; }

        [Display(Name = "Enable Analytics")]
        public bool EnableAnalytics { get; set; }

        [Display(Name = "Reorder Policy")]
        public string ReorderPolicy { get; set; } // Centralized / Decentralized

        [Display(Name = "Approval Workflow")]
        public bool ApprovalWorkflow { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }

        [Display(Name = "Last Modified Date")]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "Change History")]
        public string ChangeHistory { get; set; }
    }

}
