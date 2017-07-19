using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IUserManager
    {
        Task<Tuple<bool, string[]>> CreateUserAsync(UserDTO userObj);
        Task<Tuple<bool, string[]>> CreateRoleAsync(RoleDTO roleObj);
    }
}
