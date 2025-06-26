
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPInventoryPurchesSystems.Models.Master
{
  

public class Vendor
{
    [Key]
    [Required]
    [Display(Name = "Vendor Code")]
    public string VendorCode { get; set; }

    [Required]
    [Display(Name = "Vendor Name")]
    public string VendorName { get; set; }

    [Display(Name = "Vendor Type")]
    public string VendorType { get; set; }

    [Display(Name = "Status")]
    public string Status { get; set; }

    [Display(Name = "Contact Person Name")]
    public string ContactPersonName { get; set; }

    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }

    [Display(Name = "Email Address")]
    public string EmailAddress { get; set; }

    [Display(Name = "Fax Number")]
    public string FaxNumber { get; set; }

    public string Website { get; set; }

    public string BillingAddress { get; set; }

    public string ShippingAddress { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public string PostalCode { get; set; }

    public string Currency { get; set; }

    [Display(Name = "Payment Terms")]
    public string PaymentTerms { get; set; }

    public decimal CreditLimit { get; set; }

    [Display(Name = "Bank Account Details")]
    public string BankAccountDetails { get; set; }

    [Display(Name = "Tax Identification Number")]
    public string TaxIdentificationNumber { get; set; }

    [Display(Name = "PAN Number")]
    public string PANNumber { get; set; }

    [Display(Name = "GST/VAT Registration")]
    public string GSTVATRegistration { get; set; }

    public string MSMECertificate { get; set; }

    public string ISOCertification { get; set; }

    public string ContractAgreement { get; set; }

    [Display(Name = "Blacklist Status")]
    public bool BlacklistStatus { get; set; }

    [Display(Name = "Last Purchase Date")]
    public DateTime LastPurchaseDate { get; set; }

    public decimal TotalSpend { get; set; }

    public int AverageLeadTime { get; set; }

    public int DeliveryReliabilityScore { get; set; }

    public int QualityRating { get; set; }

    public string VendorItemCodes { get; set; }

    public string LinkedERPItems { get; set; }

    public string PreferredItems { get; set; }

    public string Remarks { get; set; }

    [Display(Name = "Document Uploads")]
    public string DocumentUploads { get; set; }

    public string CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public string LastModifiedBy { get; set; }

    public DateTime LastModifiedDate { get; set; }
}

}
