using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Ass02Solution_NguyenTuanKhai_SE151228.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public int? CategoryId { get; set; }
        public int? SupplierId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int QuantityPerUnit { get; set; }
        public string ProductImage { get; set; }

        [NotMapped]
        public Microsoft.AspNetCore.Http.IFormFile file { get; set; }

        public virtual Catergory Category { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
