using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IdentiyModels.Managers
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store) : base(store)
        {
        }

        public static ApplicationRoleManager Create(TAD db)
        {
            var appRoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            return appRoleManager;
        }
    }
}
