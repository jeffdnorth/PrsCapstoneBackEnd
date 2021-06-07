using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrsCapstoneBackEnd.Models
{
    public class RequestLine
    {
        public int Id { get; set; }

        //FK s
        public virtual Request Request { get; set; }
        public int RequestId { get; set; }

        public virtual Product Product { get; set; }
        public int ProductId { get; set; }


        //def to 1 
        [Required]
        public int Quantity { get; set; } = 1;
    }



    /*
     * Quantity must be greater than or equal to zero (cannot be negative)
There should be a virtual Product instance in the RequestLine to hold the FK instance when reading a RequestLine for the Product
     */
}
