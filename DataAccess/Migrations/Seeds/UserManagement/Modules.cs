using DataAccess.DataAccess;
using DataAccess.IdentiyModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Migrations.Seeds.UserManagement
{
    public class Modules : ISeed
    {
        public const string UserModule = "UserModule";
        public const string RoleModule = "RoleModule";

        public void SeedData(TAD context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var getUser = new ApplicationUser();
            getUser = userManager.FindByName("893569524V");

            context.Modules.AddRange(new List<Module>
            {
                new Module
                {
                    Name = UserModule,
                    CreatedBy = getUser.Id,
                    CreatedDate = DateTime.Now,
                    Status = true
                },
                new Module
                {
                    Name = RoleModule,
                    CreatedBy = getUser.Id,
                    CreatedDate = DateTime.Now,
                    Status = true
                }
            });

            context.SaveChanges();
        }
    }
}
