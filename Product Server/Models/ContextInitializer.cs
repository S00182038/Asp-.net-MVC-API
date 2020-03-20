using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Product_Server.Models
{
    public class ContextInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var manager =
                new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(context));

            var roleManager =
                new RoleManager<IdentityRole>(
                    new RoleStore<IdentityRole>(context));

            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "Purchases Manager" }
                );


            PasswordHasher ps = new PasswordHasher();


            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    UserName = "fflynstone",
                    Email = "Flintstone.fred@itsligo.ie",
                    FirstName = "Fred",
                    LastName = "Flintstone",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PasswordHash = ps.HashPassword("Flint$12345")
                });
            context.SaveChanges();

            ApplicationUser admin = manager.FindByEmail("Flintstone.fred@itsligo.ie");
            if (admin != null)
            {
                manager.AddToRoles(admin.Id, new string[] { "Purchases Manager" });
            }

            base.Seed(context);
        }
    }
}