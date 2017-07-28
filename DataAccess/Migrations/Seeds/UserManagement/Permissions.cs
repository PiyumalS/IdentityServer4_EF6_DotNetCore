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
    public class Permissions : ISeed
    {
        public const string ViewUsers = "ViewUsers";
        public const string AddUsers = "AddUsers";
        public const string DeleteUsers = "DeleteUsers";

        public void SeedData(TAD context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var getUser = new ApplicationUser();
            getUser = userManager.FindByName("893569524V");

            context.Permissions.AddRange(new List<Permission>
            {
                new Permission
                {
                    Name = ViewUsers,
                    CreatedBy = getUser.Id,
                    CreatedDate = DateTime.Now,
                    Status = true,
                    ModuleId = 1
                },
                new Permission
                {
                    Name = AddUsers,
                    CreatedBy = getUser.Id,
                    CreatedDate = DateTime.Now,
                    Status = true,
                    ModuleId = 1
                },
                new Permission
                {
                    Name = DeleteUsers,
                    CreatedBy = getUser.Id,
                    CreatedDate = DateTime.Now,
                    Status = true,
                    ModuleId = 1
                }
            });

            context.SaveChanges();
        }
    }
}
