using DataAccess.IdentiyModels;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccess
{
    public interface IUserAccess
    {
        Task<Tuple<bool, string[]>> CreateUserAsync(UserDTO user, IEnumerable<string> roles, string password);
        Task<Tuple<bool, string[]>> CreateRoleAsync(RoleDTO role, IEnumerable<string> claims);
        Task<Tuple<bool, string[]>> DeleteRoleAsync(ApplicationRole role);
        Task<Tuple<bool, string[]>> DeleteRoleAsync(string roleName);
        Task<Tuple<bool, string[]>> DeleteUserAsync(ApplicationUser user);
        Task<Tuple<bool, string[]>> DeleteUserAsync(string userId);
    }
}
