using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.Classes
{
    public class UserRolesByUser
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public System.DateTime OwnedUtcDate { get; set; }

        public virtual User User { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
