using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IdentiyModels
{
    public class RolePermission
    {
        public int Id { get; set; }
        public string RoleID { get; set; }
        public string PermissionID { get; set; }
    }
}
