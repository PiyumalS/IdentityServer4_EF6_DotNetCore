using DataAccess.IdentiyModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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

            var getUser1 = new ApplicationUser();
            getUser1 = userManager.FindByName("893569525V");

            userManager.AddToRole(getUser1.Id, Roles.Admin);

            context.SaveChanges();
        }
    }
}
