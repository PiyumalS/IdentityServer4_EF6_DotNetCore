using DataAccess.IdentiyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Migrations.Seeds
{
    public class Permissions : ISeed
    {
        public const string ViewUsers = "ViewUsers";
        public const string AddUsers = "AddUsers";
        public const string DeleteUsers = "DeleteUsers";

        public void SeedData(TAD context)
        {
            context.Permissions.AddRange(new List<Permission>
            {
                new Permission
                {
                    Name = ViewUsers
                },
                new Permission
                {
                    Name = AddUsers
                },
                new Permission
                {
                    Name = DeleteUsers
                }
            });

            context.SaveChanges();
        }
    }
}
