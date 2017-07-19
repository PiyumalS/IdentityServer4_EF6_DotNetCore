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
            ApplicationUser appUser = new ApplicationUser();

            appUser.AccessFailedCount = usr.AccessFailedCount;
            appUser.Email = usr.Email;

            if (!String.IsNullOrEmpty(usr.Id))
            {
                appUser.Id = usr.Id;
            }

            appUser.EmailConfirmed = usr.EmailConfirmed;
            appUser.IsFirstAttempt = usr.IsFirstAttempt;
            appUser.IsTempararyPassword = usr.IsTempararyPassword;
            appUser.LockoutEnabled = usr.LockoutEnabled;
            appUser.LockoutEndDateUtc = usr.LockoutEndDateUtc;
            appUser.PasswordHash = usr.PasswordHash;
            appUser.PhoneNumber = usr.PhoneNumber;
            appUser.PhoneNumberConfirmed = usr.PhoneNumberConfirmed;
            appUser.SecurityStamp = usr.SecurityStamp;
            appUser.TwoFactorEnabled = usr.TwoFactorEnabled;
            appUser.UserName = usr.NationalID;
            appUser.ActiveStatus = usr.ActiveStatus;
            return appUser;
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
                NationalID = appUser.UserName,
                ActiveStatus = appUser.ActiveStatus

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
