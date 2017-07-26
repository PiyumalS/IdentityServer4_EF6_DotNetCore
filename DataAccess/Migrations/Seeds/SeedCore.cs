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
            seedList.Add(new Permissions());

            foreach (var seedResource in seedList)
            {
                seedResource.SeedData(context);
            }
        }
    }
}
