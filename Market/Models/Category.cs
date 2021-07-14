using System;
using System.Collections.Generic;

#nullable disable

namespace Market.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoriesItems = new HashSet<CategoriesItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public virtual ICollection<CategoriesItem> CategoriesItems { get; set; }
    }
}
