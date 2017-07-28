using DataAccess.IdentiyModels;
using DataAccess.Migrations.Seeds;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Migrations.Seeds.UserManagement
{
    public class Users : ISeed
    {
        public void SeedData(TAD context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var getUser = new ApplicationUser();

            var user = new ApplicationUser
            {
                UserName = "893569524V",
                Email = "jkcssuperadmin@jkcsworld.com",
                EmailConfirmed = true,
                FirstName = "Super",
                LastName = "Admin",
                ActiveStatus = true,
                IsTempararyPassword = true,
                IsFirstAttempt = true,
                PhoneNumber = "0776532148",
                LockoutEnabled = false,
                CreatedBy = "SuperAdmin",
                CreatedDate = DateTime.Now,
                TermsAndConditionStatus = true,
                PhoneNumberConfirmed = true
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher();
                var hashed = password.HashPassword("12345");
                user.PasswordHash = hashed;

                userManager.Create(user);
                context.SaveChanges();
            }
        }
    }
}
