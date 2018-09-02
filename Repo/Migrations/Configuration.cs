namespace Repo.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Repo.Models.LibraryContext>
    {
        public Configuration()
        {
        
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Repo.Models.LibraryContext context)
        {
            if (System.Diagnostics.Debugger.IsAttached == false)
                System.Diagnostics.Debugger.Launch();
            SeedRoles(context);
            SeedUsers(context);
            SeedCategories(context);
            SeedBooks(context);
            SeedOrders(context);


        }

        private void SeedOrders(LibraryContext context)
        {
            var idUser = context.Set<User>().Where(u => u.UserName == "Admin").FirstOrDefault().Id;
            var order = new Order()
            {
                Id = 1,
                UserId = idUser,
                BookId = 1,
                OrderDate = DateTime.Now,
                ReceptionDate = DateTime.Now.AddDays(2),
                ReturnDate = DateTime.Now.AddDays(30)
            };
            context.Set<Order>().AddOrUpdate(order);
            context.SaveChanges();
        }

        private void SeedCategories(LibraryContext context)
        {
            var cat1 = new Category()
            {
                Id = 1,
                Name = "Coursebook"
            };
            context.Set<Category>().AddOrUpdate(cat1);

            var cat2 = new Category()
            {
                Id = 2,
                Name = "Novel"
            };
            context.Set<Category>().AddOrUpdate(cat2);

            var cat3 = new Category()
            {
                Id = 3,
                Name = "Biography"
            };
            context.Set<Category>().AddOrUpdate(cat3);

            context.SaveChanges();

        }

        private void SeedBooks(LibraryContext context)
        {
            var book1 = new Book()
            {
                Id = 1,
                CategoryId = 1,
                Isbn = "9788325698679",
                Title = "THINKING IN JAVA",
                Author = "Eckel Bruce",
                Page = 1256,
                Publisher = "Helion",
                PublicationYear = 2017,
                Available = "Yes"
            };
            context.Set<Book>().AddOrUpdate(book1);

            var book2 = new Book()
            {
                Id = 2,
                CategoryId = 2,
                Isbn = "9788127698879",
                Title = "Crime and Punishment",
                Author = "Fyodor Dostoyevsky",
                Page = 320,
                Publisher = "Bottom Of The Hill Publishing",
                PublicationYear = 2005,
                Available = "Yes"
             
            };
            context.Set<Book>().AddOrUpdate(book2);

            var book3 = new Book()
            {
                Id = 3,
                CategoryId = 3,
                Isbn = "9788727658809",
                Title = "Steve Jobs",
                Author = "Isaacson Walter",
                Page = 736,
                Publisher = "Insignis",
                PublicationYear = 2015,
                Available = "Yes"
            };
            context.Set<Book>().AddOrUpdate(book3);
            context.SaveChanges();
        }


        private void SeedRoles(LibraryContext context)
        {
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>
                (new RoleStore<IdentityRole>());

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Librarian"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Librarian";
                roleManager.Create(role);
            }
        }

        private void SeedUsers(LibraryContext context)
        {
            var store = new UserStore<User>(context);
            var manager = new UserManager<User>(store);

            if (!context.Users.Any(u =>u.UserName == "Admin"))
            {
                var user = new User { UserName = "Admin" };
                var result = manager.Create(user, "admin123");
                if (result.Succeeded)
                    manager.AddToRole(user.Id, "Admin");
            }
            if(!context.Users.Any(u=>u.UserName == "Bartek"))
            {
                var user = new User { UserName = "bartek@library.pl" };
                var result = manager.Create(user, "bartek123");
                if (result.Succeeded)
                    manager.AddToRole(user.Id, "Admin");

            }
            if (!context.Users.Any(u => u.UserName == "KatKowalska"))
            {
                var user = new User { UserName = "kkowalska@library.pl" };
                var result = manager.Create(user, "kowalska123");
                if (result.Succeeded)
                    manager.AddToRole(user.Id, "Librarian");
            }
        }

       



    }
}
