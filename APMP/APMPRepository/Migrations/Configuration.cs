namespace APMPRepository.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<APMPRepository.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "p.monti@apmpelectrique.ca"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { Email = "p.monti@apmpelectrique.ca", UserName = "p.monti@apmpelectrique.ca",
                    FirstName = "Pascal", LastName = "Monti", 
                    PhoneNb = "514 519-3110", Address = "17475 rue des loisirs", City="Mirabel",
                    PostalCode = "J7J-2K1", State= "Qc", SkillLevel = SkillLevel.COMPAGNON, EmailConfirmed = true };

                manager.Create(user, "Test1234");
                manager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
