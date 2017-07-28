using DataAccess.IdentiyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Migrations.Seeds
{
    public class Modules : ISeed
    {
        public const string UserModule = "UserModule";
        public const string RoleModule = "RoleModule";

        public void SeedData(TAD context)
        {
            context.Modules.AddRange(new List<Module>
            {
                new Module
                {
                    Name = UserModule,
                    CreatedBy = "SuperAdmin",
                    CreatedDate = DateTime.Now,
                    Status = true
                },
                new Module
                {
                    Name = RoleModule,
                    CreatedBy = "SuperAdmin",
                    CreatedDate = DateTime.Now,
                    Status = true
                }
            });

            context.SaveChanges();
        }
    }
}
