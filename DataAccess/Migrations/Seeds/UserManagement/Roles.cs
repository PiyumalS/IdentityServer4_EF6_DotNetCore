using DataAccess.IdentiyModels;
using DataAccess.Migrations.Seeds;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Migrations.Seeds.UserManagement
{
    public class Roles : ISeed
    {
        public static string SuperAdmin = "SuperAdmin";
        public static string Admin = "Admin";

        public void SeedData(TAD context)
        {
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var getUser = new ApplicationUser();
            getUser = userManager.FindByName("893569524V");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new ApplicationRole
                {
                    Name = SuperAdmin,
                    CreatedBy = getUser.Id,
                    CreatedDate = DateTime.Now,
                    Status = true
                });

                roleManager.Create(new ApplicationRole
                {
                    Name = Admin,
                    CreatedBy = getUser.Id,
                    CreatedDate = DateTime.Now,
                    Status = true
                });
            }

            context.SaveChanges();
        }
    }
}
