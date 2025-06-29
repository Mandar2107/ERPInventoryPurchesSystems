using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPInventoryPurchesSystems.Models.Master
{
    public class Vendor
    {
        [Key]
        [Required]
        public string VendorCode { get; set; }

        [Required]
        public string VendorName { get; set; }

        public string VendorType { get; set; }

        public string Status { get; set; }

        public string ContactPersonName { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string FaxNumber { get; set; }

        public string Website { get; set; }

        public string BillingAddress { get; set; }

        public string ShippingAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public string Currency { get; set; }

        public string PaymentTerms { get; set; }

        public decimal CreditLimit { get; set; }

        public string BankAccountDetails { get; set; }

        public string TaxIdentificationNumber { get; set; }

        public string PANNumber { get; set; }

        public string GSTVATRegistration { get; set; }

        public string MSMECertificate { get; set; }

        public string ISOCertification { get; set; }

        public string ContractAgreement { get; set; }

        public bool BlacklistStatus { get; set; }

        public DateTime LastPurchaseDate { get; set; }

        public decimal TotalSpend { get; set; }

        public int AverageLeadTime { get; set; }

        public int DeliveryReliabilityScore { get; set; }

        public int QualityRating { get; set; }

        public string VendorItemCodes { get; set; }

        public string LinkedERPItems { get; set; }

        public string PreferredItems { get; set; }

        public string Remarks { get; set; }


        [NotMapped]
        public IFormFile DocumentUploads { get; set; }


        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}
