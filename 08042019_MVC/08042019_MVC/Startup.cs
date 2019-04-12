using System;
using Microsoft.Owin;
using Owin;
using _08042019_MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_08042019_MVC.Startup))]
namespace _08042019_MVC
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUser(); // yazdım
        }

        private void createRolesandUser()
        {
            ApplicationDbContext nt = new ApplicationDbContext();

            var roleManager = new RoleManager <IdentityRole> (new RoleStore<IdentityRole>(nt));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(nt));
            if(!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
                var user = new ApplicationUser();
                user.UserName = "gulsummalbayrak@gmail.com";
                user.Email = "gulsummalbayrak@gmail.com";

                string sifre = "123Gulsum.";
                    
                var kullanici = userManager.Create(user, sifre);
                if(kullanici.Succeeded)
                {
                   var sonuc = userManager.AddToRole(user.Id, "Admin");


                }
                if (!roleManager.RoleExists("Uye"))
                {
                    var role1 = new IdentityRole();
                    role1.Name = "Uye";
                    roleManager.Create(role1);
                }





            }
        }


    }
}
