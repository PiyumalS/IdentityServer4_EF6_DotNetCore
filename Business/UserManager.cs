using DataAccess.DataAccess;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business
{
    public class UserManager : IUserManager
    {
        private readonly IUserAccess _userAccess;

        public UserManager(IUserAccess usrAccess)
        {
            _userAccess = usrAccess;
        }

        public async Task<Tuple<bool, string[]>> CreateRoleAsync(RoleDTO roleObj, IEnumerable<int> claims)
        {
            var createdRole = await _userAccess.CreateRoleAsync(roleObj, claims);
            return Tuple.Create(createdRole.Item1, new string[] { createdRole.Item2.ToString() });
        }

        public async Task<Tuple<bool, string[]>> CreateUserAsync(UserDTO userObj, IEnumerable<string> roles)
        {
            var createdUser = await _userAccess.CreateUserAsync(userObj, roles, userObj.Password);
            return Tuple.Create(createdUser.Item1, new string[] { createdUser.Item2.ToString() });
        }

        public UserDTO FindUserByID(UserDTO user)
        {
            return _userAccess.FindUserByID(user);
        }

        public Task<Tuple<UserDTO, string[], List<string>>> FindUserRolesPermissions(UserDTO user)
        {
            return _userAccess.FindUserRolesPermissions(user);
        }

        public async Task<Tuple<bool, string[]>> LoginAsync(UserDTO user)
        {
            var loginResult = await _userAccess.LoginAsync(user);
            return Tuple.Create(loginResult.Item1, new string[] { loginResult.Item2[0].ToString() });
        }
    }
}
