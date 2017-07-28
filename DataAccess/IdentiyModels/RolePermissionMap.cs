using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IdentiyModels
{
    public class RolePermissionMap
    {
        public int Id { get; set; }
        [Required]
        public string RoleId { get; set; }
        [Required]
        public int PermissionId { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; }

        [ForeignKey("RoleId")]
        public ApplicationRole ApplicationRole { get; set; }
    }
}
