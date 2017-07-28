using DataAccess.DataAccess;
using DataAccess.IdentiyModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Migrations.Seeds
{
    internal interface ISeed
    {
        void SeedData(TAD context);
    }
}
