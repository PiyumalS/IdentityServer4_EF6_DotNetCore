using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IdentiyModels.Utilities
{
    public class ObjectMapper
    {
        public ApplicationUser ConvertUserToIdentityUser(UserDTO usr)
        {
            return new ApplicationUser()
            {
                AccessFailedCount = usr.AccessFailedCount,
                Email = usr.Email,
                //Id = usr.Id,
                EmailConfirmed = usr.EmailConfirmed,
                IsFirstAttempt = usr.IsFirstAttempt,
                IsTempararyPassword = usr.IsTempararyPassword,
                LockoutEnabled = usr.LockoutEnabled,
                LockoutEndDateUtc = usr.LockoutEndDateUtc,
                PasswordHash = usr.PasswordHash,
                PhoneNumber = usr.PhoneNumber,
                PhoneNumberConfirmed = usr.PhoneNumberConfirmed,
                SecurityStamp = usr.SecurityStamp,
                TwoFactorEnabled = usr.TwoFactorEnabled,
                UserName = usr.NationalID

            };
        }


        public UserDTO ConvertIdentityUserToDomain(ApplicationUser appUser)
        {
            return new UserDTO()
            {
                AccessFailedCount = appUser.AccessFailedCount,
                Email = appUser.Email,
                Id = appUser.Id,
                EmailConfirmed = appUser.EmailConfirmed,
                IsFirstAttempt = appUser.IsFirstAttempt,
                IsTempararyPassword = appUser.IsTempararyPassword,
                LockoutEnabled = appUser.LockoutEnabled,
                LockoutEndDateUtc = appUser.LockoutEndDateUtc,
                PasswordHash = appUser.PasswordHash,
                PhoneNumber = appUser.PhoneNumber,
                PhoneNumberConfirmed = appUser.PhoneNumberConfirmed,
                SecurityStamp = appUser.SecurityStamp,
                TwoFactorEnabled = appUser.TwoFactorEnabled,
                NationalID = appUser.UserName

            };
        }

        public ApplicationRole ConvertRoleToIdentityRole(RoleDTO role)
        {
            return new ApplicationRole()
            {
                Name = role.Name                
            };
        }


        public RoleDTO ConvertIdentityRoleToDomain(ApplicationRole appRole)
        {
            return new RoleDTO()
            {
                Name = appRole.Name,
                Id = appRole.Id
            };
        }
    }
}
