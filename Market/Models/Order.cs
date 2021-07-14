using System;
using System.Collections.Generic;

#nullable disable

namespace Market.Models
{
    public partial class Order
    {
        public Order()
        {
            OrdersItems = new HashSet<OrdersItem>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public int StatusId { get; set; }
        public int ManagerId { get; set; }

        public virtual User Manager { get; set; }
        public virtual OrderStatus Status { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrdersItem> OrdersItems { get; set; }
    }
}
