using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPInventoryPurchesSystems.Models.Master
{
    public class User
    {
        [Key]
        [Required]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Designation")]
        public string Designation { get; set; }

        [Display(Name = "Department")]
        public string DepartmentCode { get; set; } // Foreign key

        [ForeignKey("DepartmentCode")]
        public Department Department { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; } // Active / Inactive

        [Display(Name = "User Role")]
        public string UserRole { get; set; } // Admin, Approver, Viewer, Operator

        [Display(Name = "Access Level")]
        public string AccessLevel { get; set; } // Module-wise permissions

        [Display(Name = "Login Type")]
        public string LoginType { get; set; } // Internal / External / API

        [Display(Name = "Authentication Method")]
        public string AuthenticationMethod { get; set; } // Password / SSO / Biometric

        public string Language { get; set; }

        public string TimeZone { get; set; }

        [Display(Name = "Notification Preferences")]
        public string NotificationPreferences { get; set; } // Email, SMS, In-app

        [Display(Name = "Last Login Date")]
        public DateTime LastLoginDate { get; set; }

        [Display(Name = "Password Expiry Date")]
        public DateTime PasswordExpiryDate { get; set; }

        [Display(Name = "Account Lock Status")]
        public bool AccountLockStatus { get; set; }

        [Display(Name = "Audit Logs Enabled")]
        public bool AuditLogsEnabled { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Last Modified By")]
        public string LastModifiedBy { get; set; }

        [Display(Name = "Last Modified Date")]
        public DateTime LastModifiedDate { get; set; }
    }
}
