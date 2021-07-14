using System;
using System.Collections.Generic;

#nullable disable

namespace Market.Models
{
    public partial class UsersRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int? SellerId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Seller Seller { get; set; }
        public virtual User User { get; set; }
    }
}
