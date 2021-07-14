using System;
using System.Collections.Generic;

#nullable disable

namespace Market.Models
{
    public partial class Seller
    {
        public Seller()
        {
            Items = new HashSet<Item>();
            UsersRoles = new HashSet<UsersRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<UsersRole> UsersRoles { get; set; }
    }
}
