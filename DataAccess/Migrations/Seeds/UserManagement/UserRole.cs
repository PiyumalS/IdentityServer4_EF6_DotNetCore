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
    public class UserRole : ISeed
    {
        public void SeedData(TAD context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var getUser = new ApplicationUser();
            getUser = userManager.FindByName("893569524V");

            userManager.AddToRole(getUser.Id, Roles.SuperAdmin);

            context.SaveChanges();
        }
    }
}
