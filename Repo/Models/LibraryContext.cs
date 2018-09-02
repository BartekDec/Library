using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.ModelConfiguration.Conventions;
using Repo.IRepo;

namespace Repo.Models
{
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
 

    public class LibraryContext : IdentityDbContext, ILibraryContext
    {
        public LibraryContext()
            : base("DefaultConnection")
        {
        }

        public static LibraryContext Create()
        {
            return new LibraryContext();
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Order> Order { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Order>().HasRequired(x => x.User).WithMany(x => x.Order)
                .HasForeignKey(x => x.UserId).WillCascadeOnDelete(true);

            modelBuilder.Entity<Order>().HasRequired(x => x.Book).WithMany(x => x.Order)
                .HasForeignKey(x => x.BookId).WillCascadeOnDelete(true);
        }
    }
}