using DataAccess.DataAccess;
using DataAccess.IdentiyModels;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<Tuple<bool, string[]>> CreateRoleAsync(RoleDTO roleObj)
        {
            var createdRole = await _userAccess.CreateRoleAsync(roleObj, null);
            return Tuple.Create(createdRole.Item1, new string[] { createdRole.Item2.ToString() });
        }

        public async Task<Tuple<bool, string[]>> CreateUserAsync(UserDTO userObj)
        {
            var createdUser = await _userAccess.CreateUserAsync(userObj, null, userObj.Password);
            return Tuple.Create(createdUser.Item1, new string[] { createdUser.Item2.ToString() });
        }
    }
}
