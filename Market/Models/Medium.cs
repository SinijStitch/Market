using System;
using System.Collections.Generic;

#nullable disable

namespace Market.Models
{
    public partial class Medium
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string Extentions { get; set; }
        public int ItemId { get; set; }

        public virtual Item Item { get; set; }
    }
}
