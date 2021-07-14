using System;
using System.Collections.Generic;

#nullable disable

namespace Market.Models
{
    public partial class User
    {
        public User()
        {
            Buskets = new HashSet<Busket>();
            OrderManagers = new HashSet<Order>();
            OrderUsers = new HashSet<Order>();
            UsersRoles = new HashSet<UsersRole>();
        }

        public int Id { get; set; }

        //public int CurrentSellerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string Mail { get; set; }
        public byte[] Image { get; set; }

        public virtual ICollection<Busket> Buskets { get; set; }
        public virtual ICollection<Order> OrderManagers { get; set; }
        public virtual ICollection<Order> OrderUsers { get; set; }
        public virtual ICollection<UsersRole> UsersRoles { get; set; }
    }
}
