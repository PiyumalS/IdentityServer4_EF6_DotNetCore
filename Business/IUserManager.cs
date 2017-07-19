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
        Task<Tuple<bool, string[]>> CreateUserAsync(UserDTO userObj, IEnumerable<string> roles);
        Task<Tuple<bool, string[]>> CreateRoleAsync(RoleDTO roleObj);
        Task<Tuple<bool, string[]>> LoginAsync(UserDTO user);
        UserDTO FindUserByID(UserDTO user);
    }
}
