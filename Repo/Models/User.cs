using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Repo.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Order = new HashSet<Order>();
        }
        
        public string Name { get; set; }
        public string Surname { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(40)]
        public string City { get; set; }

        
        public virtual ICollection<Order> Order { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}