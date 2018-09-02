using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repo.Models
{
    public class Book
    {
        public Book()
        {
            this.Order = new HashSet<Order>();
         
        }
        [Display(Name = "Id:")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "ISBN number")]
        [MaxLength(13)]
        public string Isbn { get; set; }

        [Required]
        [Display(Name = "Title")]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Author")]
        [MaxLength(200)]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Number of pages")]
        [Range(5, 9999)]
        public int Page { get; set; }

        [Required]
        [Display(Name = "Publisher")]
        [MaxLength(50)]
        public string Publisher { get; set; }

        [Required]
        [Display(Name = "Publication year")]
        [Range(1000, 9999)]
        public int PublicationYear { get; set; }

        [Required]
        [Display(Name = "Available")] 
        [MaxLength(3)]
        public string Available  { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}