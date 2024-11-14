using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVPRO.DATA.EF.Models
{



    #region Country 
    public class CountryMetadata
    {
        public int CountryId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Country Name")]
        [DataType(DataType.Text)]
        public string CountryName { get; set; } = null!;

        [Display(Name = "Region")]
        public int? RegionId { get; set; }
    }

    #endregion

    #region Customer
    public class CustomerMetadata
    {

        public int CustomerId { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; } = null!;

        [Required]
        [MaxLength (75)]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; } = null!;

        [Required]
        [MaxLength (20)]
        [Display(Name = "Contact Phone")]
        [DataType(DataType.PhoneNumber)]
        public string ContactPhone { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [Display(Name = "Contact Email")]
        [DataType(DataType.EmailAddress)]
        public string ContactEmail { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [Display(Name = "Billing City")]
        public string BillingCity { get; set; } = null!;

        [MaxLength(2)]
        [Display(Name = "Billing State")]
        public string? BillingState { get; set; }

        [MaxLength(10)]
        [Display(Name = "Billing Postal Code")]
        [DataType(DataType.PostalCode)]
        public string? BillingPostalCode { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }
    }
    #endregion

    #region Department
    public class DepartmentMetadata
    {
        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; } = null!;

        [Display(Name = "Location")]
        public int? LocationId { get; set; }
    }

    #endregion

    #region Employee
    public class EmployeeMetadata
    {
        public int EmployeeId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(25)]
        public string FirstName { get; set; } = null!;

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [StringLength(100)]
        public string? Position { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        [Range(0, (double)decimal.MaxValue)]
        public decimal? Salary { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        public DateTime? HireDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Termination Date")]
        [DataType(DataType.Date)]
        public DateTime? TerminationDate { get; set; }

        [StringLength(100)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(2)]
        public string? State { get; set; }

        [StringLength(10)]
        [Display(Name = "Zip")]
        [DataType(DataType.PostalCode)]
        public string? PostalCode { get; set; }

        [Display(Name = "Country")]
        public int? CountryId { get; set; }

        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Display(Name = "Manager")]
        public int? ManagerId { get; set; }
    }
    #endregion

    #region Location
    public class LocationMetadata
    {
        //Key : No annotations
        public int LocationId { get; set; }

        [Required]
        [Display(Name = "Address")]
        [MaxLength(100)]
        public string LocationAddress { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        [Display(Name = "City")]
        public string LocationCity { get; set; } = null!;

        [MaxLength(2)]
        [DataType(DataType.Text)]
        [Display(Name = "State")]
        public string? LocationState { get; set; }


        [DataType(DataType.PostalCode)]
        [MaxLength(10)]
        [Display(Name = "Postal Code")]
        public string? LocationPostalCode { get; set; }

        //Foreign Key : No annotations
        public int? CountryId { get; set; }
    }
    #endregion

    #region Order
    public class OrderMetadata
    {
        //Key : No annotations
        public int OrderId { get; set; }

        [MaxLength(50)]
        [Display(Name = "P.O. Number")]
        public string? Ponumber { get; set; }


        //Foreign Key: No annotations
        public int VendorId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        [DataType(DataType.Currency)]
        [Display(Name = "Order Total")]
        public decimal? OrderTotal { get; set; }
    }
    #endregion

    #region OrderProducts
    public class OrderProductMetadata
    {
        //Key : No annotations
        public int OrderProductId { get; set; }

        //Foreign Key: No annotations
        public int OrderId { get; set; }

        //Foreign Key: No annotations
        public int ProductId { get; set; }

        [MaxLength(2000)]
        [Display(Name = "Order Qty")]
        public short OrderQuantity { get; set; }
    }
    #endregion

    //------------------------------------------------


    public class ProductMetadata
    {
        public int ProductId { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = null!;

        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        [UIHint("MultilineText")]
        public string? Description { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        [Display(Name = "Cost Per Unit")]
        [Range(0, (double)decimal.MaxValue)]
        public decimal? CostPerUnit { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        [Display(Name = "Price Per Unit")]
        [Range(0, (double)decimal.MaxValue)]
        public decimal? PricePerUnit { get; set; }

        [MaxLength(50)]
        [Display(Name = "Unit Type")]
        public string? UnitType { get; set; }

        [Display(Name = "Units In Stock")]
        [Range(0, short.MaxValue)]
        public short? UnitsInStock { get; set; }

        [Display(Name = "Units On Order")]
        [Range(0, short.MaxValue)]
        public short? UnitsOnOrder { get; set; }

        [Display(Name = "Product Status")]
        public int? ProductStatusId { get; set; }

        [Display(Name = "Product Type")]
        public int ProductTypeId { get; set; }

        [Display(Name = "Vendor")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public int? VendorId { get; set; }

        [Display(Name = "Image")]
        [MaxLength(100)]
        public string? ProductImage { get; set; }
    }

    public class ProductStatusMetadata
    {
        public int ProductStatusId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Product Status")]
        public string ProductStatusName { get; set; } = null!;
    }

    public class ProductTypeMetadata
    {
        public int ProductTypeId { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "Product Type")]
        public string ProductTypeName { get; set; } = null!;
    }

    public class RegionMetadata
    {
        public int RegionId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Region")]
        public string RegionName { get; set; } = null!;
    }

    public class SaleMetadata
    {
        public int SaleId { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        [Display(Name = "Sale Date")]
        public DateTime SaleDate { get; set; }

        [Display(Name = "Sales Person")]
        public int SalespersonId { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        [Display(Name = "Sale Total")]
        [Range(0, (double)decimal.MaxValue)]
        public decimal? SaleTotal { get; set; }
    }
    //Team cheese ^

    public class SaleProductMetadata
    {
        public int SaleProductId { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }

        [Display(Name = "Sale Qty")]
        [Range(0, short.MaxValue)]
        [Required]
        public short SaleQuantity { get; set; }
    }

    public class VendorMetadata
    {
        public int VendorId { get; set; }

        [Required]
        [Display(Name = "Vendor")]
        [StringLength(150)]
        public string VendorName { get; set; } = null!;

        [Required]
        [Display(Name = "Vendor Contact")]
        [StringLength(75)]
        public string VendorContact { get; set; } = null!;

        [Required]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(20)]
        public string VendorPhone { get; set; } = null!;

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100)]
        public string VendorEmail { get; set; } = null!;

        [Required]
        [Display(Name = "Address")]
        [StringLength(100)]
        public string VendorAddress { get; set; } = null!;

        [Required]
        [StringLength(100)]
        [Display(Name = "City")]
        public string VendorCity { get; set; } = null!;

        [StringLength(2)]
        [Display(Name = "State")]
        public string? VendorState { get; set; }

        [StringLength(10)]
        [Display(Name = "Zip")]
        [DataType(DataType.PostalCode)]
        public string? VendorPostalCode { get; set; }
        public int CountryId { get; set; }
    }
}
