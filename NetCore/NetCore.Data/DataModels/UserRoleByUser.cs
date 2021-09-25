using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Data.DataModels
{
    public class UserRoleByUser
    {
        [Key, StringLength(50), Column(TypeName = "varchar(50")]
        public string UserId { get; set; }

        [Key, StringLength(50), Column(TypeName = "varchar(50")]
        public string RoleId { get; set; }
        
        [Required]
        public DateTime OwnedUtcDate { get; set; }
    }
}
