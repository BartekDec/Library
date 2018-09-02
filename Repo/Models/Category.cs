using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repo.Models
{
    public class Category
    {
        public Category()
        {
            this.Book = new HashSet<Book>();
        }
        [Display(Name = "Id:")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category name: ")]
        public string Name { get; set; }

        public virtual ICollection<Book> Book { get; set; }
    }
}