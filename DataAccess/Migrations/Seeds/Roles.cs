using DataAccess.IdentiyModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Migrations.Seeds
{
    public class Roles : ISeed
    {
        public static string SuperAdmin = "SuperAdmin";

        public void SeedData(TAD context)
        {
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new ApplicationRole
                {
                    Name = SuperAdmin,
                    CreatedBy = "SuperAdmin",
                    CreatedDate = DateTime.Now,
                    Status = true
                });
            }

            context.SaveChanges();
        }
    }
}
