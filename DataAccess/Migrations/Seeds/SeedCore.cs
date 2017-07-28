using DataAccess.Migrations.Seeds.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Migrations.Seeds
{
    public class SeedCore
    {
        public static void RunSeeds(TAD context)
        {
            var seedList = new List<ISeed>();
            seedList.Add(new Users());
            seedList.Add(new Roles());
            seedList.Add(new UserRole());
            seedList.Add(new Modules());
            seedList.Add(new Permissions());           
            seedList.Add(new RolePermissionMaps());

            foreach (var seedResource in seedList)
            {
                seedResource.SeedData(context);
            }
        }
    }
}
