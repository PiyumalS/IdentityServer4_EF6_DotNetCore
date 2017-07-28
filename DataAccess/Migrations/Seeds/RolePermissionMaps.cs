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
    public class RolePermissionMaps : ISeed
    {
        public void SeedData(TAD context)
        {
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            #region Super Admin

            var superAdminRole = roleManager.FindByName(Roles.SuperAdmin);

            foreach (var permission in context.Permissions)
            {
                context.RolePermissionMaps.Add(new RolePermissionMap
                {
                    RoleId = superAdminRole.Id,
                    PermissionId = permission.Id,
                    CreatedBy = "SuperAdmin",
                    CreatedDate = DateTime.Now,
                    Status = true
                });
            }

            #endregion

            context.SaveChanges();
        }
    }
}
