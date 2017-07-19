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
        Task<Tuple<bool, string[]>> LoginAsync(UserDTO user);
        UserDTO FindUserByID(UserDTO user);
        Task<Tuple<UserDTO, string[], string[]>> FindUserRolesPermissions(UserDTO user);
        Task<ApplicationRole> GetRoleByIdAsync(string roleId);
        Task<ApplicationRole> GetRoleByNameAsync(string roleName);
        Task<ApplicationRole> GetRoleLoadRelatedAsync(string roleName);
        Task<List<ApplicationRole>> GetRolesLoadRelatedAsync(int page, int pageSize);
        Task<Tuple<ApplicationUser, string[]>> GetUserAndRolesAsync(string userId);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<ApplicationUser> GetUserByUserNameAsync(string userName);
        Task<IList<string>> GetUserRolesAsync(UserDTO user);
        Task<List<Tuple<ApplicationUser, string[]>>> GetUsersAndRolesAsync(int page, int pageSize);
        Task<Tuple<bool, string[]>> ResetPasswordAsync(UserDTO user, string newPassword);
        Task<Tuple<bool, string[]>> UpdatePasswordAsync(UserDTO user, string currentPassword, string newPassword);
        Task<Tuple<bool, string[]>> UpdateRoleAsync(RoleDTO role, IEnumerable<string> claims);
        Task<Tuple<bool, string[]>> UpdateUserAsync(UserDTO user);
        Task<Tuple<bool, string[]>> UpdateUserAsync(UserDTO user, IEnumerable<string> roles);

    }
}
