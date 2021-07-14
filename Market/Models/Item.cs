using System;
using System.Collections.Generic;

#nullable disable

namespace Market.Models
{
    public partial class Item
    {
        public Item()
        {
            CategoriesItems = new HashSet<CategoriesItem>();
            Media = new HashSet<Medium>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public decimal? Price { get; set; }
        public int ManufacturerId { get; set; }
        public int SellerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
        public virtual Seller Seller { get; set; }
        public virtual ICollection<CategoriesItem> CategoriesItems { get; set; }
        public virtual ICollection<Medium> Media { get; set; }
    }
}
