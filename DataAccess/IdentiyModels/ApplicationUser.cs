using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public bool ActiveStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? LastPasswordChangeDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool TermsAndConditionStatus { get; set; }
        public DateTime? TermsAndConditionAcceptDate { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
