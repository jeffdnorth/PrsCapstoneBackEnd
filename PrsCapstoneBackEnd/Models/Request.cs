using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrsCapstoneBackEnd.Models
{
    public class Request
    {
        public int Id { get; set; }

        [Required, StringLength(80)]
        public string Description { get; set; }

        [Required, StringLength(80)]
        public string Justification { get; set; }

        [StringLength(80)]
        public String RejectionReason { get; set; }


        //def 'Pickup
        [Required, StringLength(20)]
        public string DeliveryMode { get; set; } = "Pickup";

        //def 'New'
        [Required, StringLength(10)]
        public string Status { get; set; } = "New";

        [Column(TypeName = "decimal(11,2)")]
        public decimal Total { get; set; } = 0;

        //FK
        public virtual User User { get; set; }
        public int UserId { get; set; }

        public Request() { }

        /*
         * RejectionReason must be provided when the request is rejected
The UserId is automatically set to the Id of the logged in user.
Neither Status nor Total may be set by the user. These are set by the application only.
The Total is auto calculated by adding up all the lines currently on the request
There should be a virtual User instance in the Request to hold the FK instance when reading a Request
There should be a virtual collection of RequestLine instances in the Request to hold the collection of lines related to this Request.
         */
    }
}
