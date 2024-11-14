using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DVPRO.DATA.EF.Models
{
    [ModelMetadataType(typeof(CountryMetadata))]
    public partial class Country { }

    [ModelMetadataType(typeof(CustomerMetadata))]
    public partial class Customer { }

    [ModelMetadataType(typeof(DepartmentMetadata))]
    public partial class Department { }

    [ModelMetadataType(typeof(EmployeeMetadata))]
    public partial class Employee 
    {
        public string FullName
        { 
            get
            {
                return string.Format($"{FirstName} {LastName}");
            }
        }
    }

    [ModelMetadataType(typeof(LocationMetadata))]
    public partial class Location { }

    [ModelMetadataType(typeof(OrderMetadata))]
    public partial class Order { }

    [ModelMetadataType(typeof(OrderProductMetadata))]
    public partial class OrderProduct { }

    [ModelMetadataType(typeof(ProductMetadata))]
    public partial class Product { }

    [ModelMetadataType(typeof(ProductStatusMetadata))]
    public partial class ProductStatus { }

    [ModelMetadataType(typeof(ProductTypeMetadata))]
    public partial class ProductType { }

    [ModelMetadataType(typeof(RegionMetadata))]
    public partial class Region { }

    [ModelMetadataType(typeof(SaleMetadata))]
    public partial class Sale { }

    [ModelMetadataType(typeof(SaleProductMetadata))]
    public partial class SaleProduct { }

    [ModelMetadataType(typeof(VendorMetadata))]
    public partial class Vendor { }
}
