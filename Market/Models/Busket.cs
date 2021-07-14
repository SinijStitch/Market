using System;
using System.Collections.Generic;

#nullable disable

namespace Market.Models
{
    public partial class Busket
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int? ItemsQuantity { get; set; }

        public virtual User User { get; set; }
    }
}
