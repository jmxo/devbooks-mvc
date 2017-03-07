namespace DevBooks.Web.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "DevBooks.Web.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any(u => u.UserName == "admin@devbooks.com"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Manager" });
                roleManager.Create(new IdentityRole { Name = "User" });

                var admin = new ApplicationUser { UserName = "admin@devbooks.com", Email = "admin@devbooks.com" };
                userManager.Create(admin, "password");
                userManager.AddToRole(admin.Id, "Admin");

                var user = new ApplicationUser { UserName = "user@devbooks.com", Email = "user@devbooks.com" };
                userManager.Create(user, "password");
                userManager.AddToRole(user.Id, "User");
            }
        }
    }
}
