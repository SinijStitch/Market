using System;
using System.Collections.Generic;

#nullable disable

namespace Market.Models
{
    public partial class CategoriesItem
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ItemId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Item Item { get; set; }
    }
}
