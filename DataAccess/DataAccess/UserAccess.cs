using DataAccess.IdentiyModels;
using DataAccess.IdentiyModels.Managers;
using DataAccess.IdentiyModels.Utilities;
using Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccess
{
    public class UserAccess : IUserAccess
    {
        private readonly TAD _context;

        ApplicationUserManager _appUserManager;
        ApplicationRoleManager _appRoleManager;

        public UserAccess(TAD context)
        {
            _context = context;
            _appUserManager = ApplicationUserManager.Create(_context);
            _appRoleManager = ApplicationRoleManager.Create(_context);
        }

        public async Task<Tuple<bool, string[]>> CreateRoleAsync(RoleDTO role, IEnumerable<string> claims)
        {
            ObjectMapper mapper = new ObjectMapper();
            ApplicationRole appRole = mapper.ConvertRoleToIdentityRole(role);

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {

                    var result = await _appRoleManager.CreateAsync(appRole);
                    if (!result.Succeeded)
                        return Tuple.Create(false, result.Errors.ToArray());


                    appRole = await _appRoleManager.FindByNameAsync(appRole.Name);


                    if (claims != null)
                    {
                        List<RolePermission> rolePermissionList = new List<RolePermission>();

                        foreach (string claim in claims)
                        {
                            RolePermission tmpDTO = new RolePermission();
                            tmpDTO.PermissionID = claim;
                            tmpDTO.RoleID = appRole.Id;

                            rolePermissionList.Add(tmpDTO);
                        }

                        _context.RolePermissions.AddRange(rolePermissionList);
                        await _context.SaveChangesAsync();
                        dbContextTransaction.Commit();

                    }

                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }

            }

            return Tuple.Create(true, new string[] { appRole.Id });
        }

        public async Task<Tuple<bool, string[]>> CreateUserAsync(UserDTO user, IEnumerable<string> roles, string password)
        {
            ObjectMapper mapper = new ObjectMapper();
            ApplicationUser appUser = mapper.ConvertUserToIdentityUser(user);


            try
            {

                var result = await _appUserManager.CreateAsync(appUser, password);

                if (!result.Succeeded)
                {
                    return Tuple.Create(false, result.Errors.ToArray());

                }

                appUser = await _appUserManager.FindByNameAsync(appUser.UserName);

                try
                {
                    if (roles != null)
                    {
                        result = await _appUserManager.AddToRolesAsync(user.Id, roles.ToArray());
                    }
                }
                catch
                {
                    await DeleteUserAsync(appUser);
                    throw;
                }

                if (!result.Succeeded)
                {
                    await DeleteUserAsync(appUser);
                    return Tuple.Create(false, result.Errors.ToArray());
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

            return Tuple.Create(true, new string[] { appUser.Id });


        }

        public async Task<Tuple<bool, string[]>> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _appUserManager.DeleteAsync(user);
            return Tuple.Create(result.Succeeded, result.Errors.ToArray());
        }

        public async Task<Tuple<bool, string[]>> DeleteRoleAsync(ApplicationRole role)
        {
            var result = await _appRoleManager.DeleteAsync(role);
            return Tuple.Create(result.Succeeded, result.Errors.ToArray());
        }

        public async Task<Tuple<bool, string[]>> DeleteRoleAsync(string roleName)
        {
            var role = await _appRoleManager.FindByNameAsync(roleName);

            if (role != null)
                return await DeleteRoleAsync(role);

            return Tuple.Create(true, new string[] { });
        }

        public async Task<Tuple<bool, string[]>> DeleteUserAsync(string userId)
        {
            var user = await _appUserManager.FindByIdAsync(userId);

            if (user != null)
                return await DeleteUserAsync(user);

            return Tuple.Create(true, new string[] { });
        }
        public UserDTO FindUserByID(UserDTO user)
        {
            ObjectMapper mapper = new ObjectMapper();
            ApplicationUser appUser = _appUserManager.FindById(user.Id);
            if (appUser != null)
            {
                return mapper.ConvertIdentityUserToDomain(appUser);
            }
            return null;

        }

        public async Task<Tuple<bool, string[]>> LoginAsync(UserDTO user)
        {
            var existingUser = _appUserManager.FindByNameAsync(user.NationalID).Result;

            List<string> errorList = new List<string>();

            string[] errors = new string[1];

            if (existingUser == null)
            {
                //return invalid user name or password
                errorList.Add("Invalid user name or password");
                return Tuple.Create(false, errorList.ToArray());

            }

            if (!existingUser.ActiveStatus)
            {
                //return user disabled
                errorList.Add("User disabled");
                return Tuple.Create(false, errorList.ToArray());
            }

            //can sign in part

            if (_appUserManager.SupportsUserLockout && await _appUserManager.IsLockedOutAsync(existingUser.Id))
            {
                //return user locked out
                errorList.Add("User locked");
                return Tuple.Create(false, errorList.ToArray());

            }


            //checks the password
            if (!_appUserManager.CheckPasswordAsync(existingUser, user.Password).Result)
            {
                if (_appUserManager.SupportsUserLockout)
                {
                    await _appUserManager.AccessFailedAsync(existingUser.Id); // updates the access fail count
                }

                errorList.Add("User name or password invalid");
                return Tuple.Create(false, errorList.ToArray());

            }

            if (_appUserManager.SupportsUserLockout)
            {
                await _appUserManager.ResetAccessFailedCountAsync(existingUser.Id);
            }

            errorList.Add(existingUser.Id);
            return Tuple.Create(true, errorList.ToArray());

        }

        public async Task<Tuple<UserDTO, string[], string[]>> FindUserRolesPermissions(UserDTO user)
        {
            try
            {
                var existingUser = await _context.Users.Include(u => u.Roles).Where(u => u.Id == user.Id).FirstOrDefaultAsync();

                if (existingUser == null) { return null; }

                var userRolesIds = existingUser.Roles.Select(r => r.RoleId).ToList();

                var roles = await _context.Roles.Where(r => userRolesIds.Contains(r.Id)).Select(r => r.Name).ToArrayAsync();

                var permissions = await _context.RolePermissions.Where(r => userRolesIds.Contains(r.RoleID)).Select(p => p.PermissionID).ToArrayAsync();


                ObjectMapper mapper = new ObjectMapper();

                return Tuple.Create(mapper.ConvertIdentityUserToDomain(existingUser), roles, permissions);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ApplicationRole> GetRoleByIdAsync(string roleId)
        {
            return await _appRoleManager.FindByIdAsync(roleId);
        }

        public async Task<ApplicationRole> GetRoleByNameAsync(string roleName)
        {
            return await _appRoleManager.FindByNameAsync(roleName);
        }

        public async Task<ApplicationRole> GetRoleLoadRelatedAsync(string roleName)
        {
            var role = await _appRoleManager.Roles
                .Include(r => r.Users)
                .Where(r => r.Name == roleName)
                .FirstOrDefaultAsync();

            return role;
        }

        public async Task<List<ApplicationRole>> GetRolesLoadRelatedAsync(int page, int pageSize)
        {
            IQueryable<ApplicationRole> rolesQuery = _appRoleManager.Roles
                .Include(r => r.Users)
                .OrderBy(r => r.Name);

            if (page != -1)
                rolesQuery = rolesQuery.Skip((page - 1) * pageSize);

            if (pageSize != -1)
                rolesQuery = rolesQuery.Take(pageSize);

            var roles = await rolesQuery.ToListAsync();

            return roles;
        }

        public async Task<Tuple<ApplicationUser, string[]>> GetUserAndRolesAsync(string userId)
        {
            var user = await _appUserManager.Users
                .Include(u => u.Roles)
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
                return null;

            var userRoleIds = user.Roles.Select(r => r.RoleId).ToList();

            var roles = await _appRoleManager.Roles
                .Where(r => userRoleIds.Contains(r.Id))
                .Select(r => r.Name)
                .ToArrayAsync();

            return Tuple.Create(user, roles);
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _appUserManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _appUserManager.FindByIdAsync(userId);
        }

        public async Task<ApplicationUser> GetUserByUserNameAsync(string userName)
        {
            return await _appUserManager.FindByNameAsync(userName);
        }

        public async Task<IList<string>> GetUserRolesAsync(UserDTO user)
        {
            return await _appUserManager.GetRolesAsync(user.Id);
        }

        public async Task<List<Tuple<ApplicationUser, string[]>>> GetUsersAndRolesAsync(int page, int pageSize)
        {
            IQueryable<ApplicationUser> usersQuery = _appUserManager.Users
                .Include(u => u.Roles)
                .OrderBy(u => u.UserName);

            if (page != -1)
                usersQuery = usersQuery.Skip((page - 1) * pageSize);

            if (pageSize != -1)
                usersQuery = usersQuery.Take(pageSize);

            var users = await usersQuery.ToListAsync();

            var userRoleIds = users.SelectMany(u => u.Roles.Select(r => r.RoleId)).ToList();

            var roles = await _appRoleManager.Roles
                .Where(r => userRoleIds.Contains(r.Id))
                .ToArrayAsync();

            return users.Select(u => Tuple.Create(u,
                roles.Where(r => u.Roles.Select(ur => ur.RoleId).Contains(r.Id)).Select(r => r.Name).ToArray()))
                .ToList();
        }

        public async Task<Tuple<bool, string[]>> ResetPasswordAsync(UserDTO user, string newPassword)
        {
            string resetToken = await _appUserManager.GeneratePasswordResetTokenAsync(user.Id);

            var result = await _appUserManager.ResetPasswordAsync(user.Id, resetToken, newPassword);
            if (!result.Succeeded)
                return Tuple.Create(false, result.Errors.ToArray());

            return Tuple.Create(true, new string[] { });
        }

        public async Task<Tuple<bool, string[]>> UpdatePasswordAsync(UserDTO user, string currentPassword, string newPassword)
        {
            var result = await _appUserManager.ChangePasswordAsync(user.Id, currentPassword, newPassword);
            if (!result.Succeeded)
                return Tuple.Create(false, result.Errors.ToArray());

            return Tuple.Create(true, new string[] { });
        }

        public async Task<Tuple<bool, string[]>> UpdateRoleAsync(RoleDTO role, IEnumerable<string> claims)
        {
            ObjectMapper mapper = new ObjectMapper();
            ApplicationRole appRole = mapper.ConvertRoleToIdentityRole(role);

            if (claims != null)
            {
                //string[] invalidClaims = claims.Where(c => ApplicationPermissions.GetPermissionByValue(c) == null).ToArray();
                //if (invalidClaims.Any())
                //    return Tuple.Create(false, new string[] { "The following claim types are invalid: " + string.Join(", ", invalidClaims) });
            }

            var result = await _appRoleManager.UpdateAsync(appRole);
            if (!result.Succeeded)
                return Tuple.Create(false, result.Errors.ToArray());


            if (claims != null)
            {
                //var roleClaims = (await _appRoleManager.GetClaimsAsync(role)).Where(c => c.Type == CustomClaimTypes.Permission);
                //var roleClaimValues = roleClaims.Select(c => c.Value).ToArray();

                //var claimsToRemove = roleClaimValues.Except(claims).ToArray();
                //var claimsToAdd = claims.Except(roleClaimValues).Distinct().ToArray();

                //if (claimsToRemove.Any())
                //{
                //    foreach (string claim in claimsToRemove)
                //    {
                //        result = await _roleManager.RemoveClaimAsync(role, roleClaims.Where(c => c.Value == claim).FirstOrDefault());
                //        if (!result.Succeeded)
                //            return Tuple.Create(false, result.Errors.Select(e => e.Description).ToArray());
                //    }
                //}

                //if (claimsToAdd.Any())
                //{
                //    foreach (string claim in claimsToAdd)
                //    {
                //        result = await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, ApplicationPermissions.GetPermissionByValue(claim)));
                //        if (!result.Succeeded)
                //            return Tuple.Create(false, result.Errors.Select(e => e.Description).ToArray());
                //    }
                //}
            }

            return Tuple.Create(true, new string[] { });
        }

        public async Task<Tuple<bool, string[]>> UpdateUserAsync(UserDTO user)
        {
            return await UpdateUserAsync(user, null);
        }

        public async Task<Tuple<bool, string[]>> UpdateUserAsync(UserDTO user, IEnumerable<string> roles)
        {
            ObjectMapper mapper = new ObjectMapper();
            ApplicationUser appUser = mapper.ConvertUserToIdentityUser(user);

            var result = await _appUserManager.UpdateAsync(appUser);
            if (!result.Succeeded)
                return Tuple.Create(false, result.Errors.ToArray());


            if (roles != null)
            {
                var userRoles = await _appUserManager.GetRolesAsync(user.Id);

                var rolesToRemove = userRoles.Except(roles).ToArray();
                var rolesToAdd = roles.Except(userRoles).Distinct().ToArray();

                if (rolesToRemove.Any())
                {
                    result = await _appUserManager.RemoveFromRolesAsync(user.Id, rolesToRemove);
                    if (!result.Succeeded)
                        return Tuple.Create(false, result.Errors.ToArray());
                }

                if (rolesToAdd.Any())
                {
                    result = await _appUserManager.AddToRolesAsync(user.Id, rolesToAdd);
                    if (!result.Succeeded)
                        return Tuple.Create(false, result.Errors.ToArray());
                }
            }

            return Tuple.Create(true, new string[] { });
        }
    }
}
