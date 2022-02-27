using System;
using System.Collections.Generic;

#nullable disable

namespace Ass02Solution_NguyenTuanKhai_SE151228.Models
{
    public partial class Catergory
    {
        public Catergory()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
