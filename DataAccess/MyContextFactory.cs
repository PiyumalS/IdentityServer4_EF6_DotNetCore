using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MyContextFactory : IDbContextFactory<TAD>
    {
        public TAD Create()
        {
            return new TAD("TAD");
        }
    }
}
