using Repo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.IRepo
{
   public interface ILibraryContext
    {
         DbSet<Category> Category { get; set; }
         DbSet<User> User { get; set; }
         DbSet<Book> Book { get; set; }
         DbSet<Order> Order { get; set; }
         int SaveChanges();
        //Database property using to log in query to db
        Database Database { get; }

        DbEntityEntry Entry(object entity);

    }
}
