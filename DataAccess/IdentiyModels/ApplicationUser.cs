using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IdentiyModels
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsFirstAttempt { get; set; }
        public bool IsTempararyPassword { get; set; }
        public bool ActiveStatus { get; set; }
        public string FullName { get; set; }
    }
}
