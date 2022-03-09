using System;
using System.Collections.Generic;

#nullable disable

namespace Ass03Solution_NguyenTuanKhai_SE151228.Models
{
    public partial class PostCategory
    {
        public PostCategory()
        {
            Posts = new HashSet<Post>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
