using Hikaya.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(Hikaya.Startup))]
namespace Hikaya
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                IdentityRole adminRole = new IdentityRole();
                adminRole.Name = "Admin";
                roleManager.Create(adminRole);
                ApplicationUser adminUser = new ApplicationUser();
                adminUser.UserName = "admin@test.test";
                adminUser.Email = "admin@test.test";
                string pass = "P@ssw0rd";
                IdentityResult result = userManager.Create(adminUser, pass);

                //لتحقق من انشاء المستخدم بنجاح ثم اضافته الى دور مدير التطبيق
                if (result.Succeeded)
                {
                    userManager.AddToRole(adminUser.Id, "Admin");
                }

            }
        }
    }

}
