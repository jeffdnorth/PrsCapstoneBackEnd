using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrsCapstoneBackEnd.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string PartNbr { get; set; }

        [Required, StringLength(30)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(11,2)")]
        public decimal Price { get; set; }

        [Required, StringLength(30)]
        public string Unit { get; set; }

        [StringLength(255)]
        public string PhotoPath { get; set; }

        public virtual Vendor Vendor { get; set; }
        public int  VendorId { get; set; }

        public  Product() { }

        /*
         * The PartNbr column must be unique for all rows though it is not the PK. It represents the vendors identifier for the product
The Name is the column displayed to the user and is the name of the product given by the company.
The VendorId points to the vendor that supplies the product.
There should be a virtual Vendor instance in the Product to hold the FK instance when reading a Product
         */
    }
}
