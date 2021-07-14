using System;
using System.Collections.Generic;

#nullable disable

namespace Market.Models
{
    public partial class Role
    {
        public static RolesEnum ActiveUserRole = RolesEnum.User ;
        public Role()
        {
            UsersRoles = new HashSet<UsersRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UsersRole> UsersRoles { get; set; }
    }
}
