using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repo.Models
{
    public class Order
    {

        [Display(Name = "Id:")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Order date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public System.DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Reception date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public System.DateTime ReceptionDate { get; set; }

        [Required]
        [Display(Name = "Return date:")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public System.DateTime ReturnDate { get; set; }
       
        public string UserId { get; set; }
        public int BookId { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}