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
            appUser.UserName = usr.UserName;
            appUser.ActiveStatus = usr.ActiveStatus;
            appUser.FirstName = usr.FirstName;
            appUser.LastName = usr.LastName;
            appUser.LastPasswordChangeDate = usr.LastPasswordChangeDate;
            appUser.LastLoginDate = usr.LastLoginDate;
            appUser.TermsAndConditionStatus = usr.TermsAndConditionStatus;
            appUser.TermsAndConditionAcceptDate = usr.TermsAndConditionAcceptDate;
            appUser.CreatedBy = usr.CreatedBy;
            appUser.CreatedDate = usr.CreatedDate;
            appUser.UpdatedBy = usr.UpdatedBy;
            appUser.UpdatedDate = usr.UpdatedDate;

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
                UserName = appUser.UserName,
                ActiveStatus = appUser.ActiveStatus,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                LastPasswordChangeDate = appUser.LastPasswordChangeDate,
                LastLoginDate = appUser.LastLoginDate,
                TermsAndConditionStatus = appUser.TermsAndConditionStatus,
                TermsAndConditionAcceptDate = appUser.TermsAndConditionAcceptDate,
                CreatedBy = appUser.CreatedBy,
                CreatedDate = appUser.CreatedDate,
                UpdatedBy = appUser.UpdatedBy,
                UpdatedDate = appUser.UpdatedDate
        };
        }

        public ApplicationRole ConvertRoleToIdentityRole(RoleDTO role)
        {
            return new ApplicationRole()
            {
                Name = role.Name,
                CreatedBy = role.CreatedBy,
                CreatedDate = role.CreatedDate,
                UpdatedBy = role.UpdatedBy,
                UpdatedDate = role.UpdatedDate,
                Status = role.Status,
                RoleCode = role.RoleCode,
                Description = role.Description
        };
        }


        public RoleDTO ConvertIdentityRoleToDomain(ApplicationRole appRole)
        {
            return new RoleDTO()
            {
                Name = appRole.Name,
                Id = appRole.Id,
                CreatedBy = appRole.CreatedBy,
                CreatedDate = appRole.CreatedDate,
                UpdatedBy = appRole.UpdatedBy,
                UpdatedDate = appRole.UpdatedDate,
                Status = appRole.Status,
                RoleCode = appRole.RoleCode,
                Description = appRole.Description
            };
        }
    }
}
