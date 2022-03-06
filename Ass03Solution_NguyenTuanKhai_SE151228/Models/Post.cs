using System;
using System.Collections.Generic;

#nullable disable

namespace Ass03Solution_NguyenTuanKhai_SE151228.Models
{
    public partial class Post
    {
        public int PostId { get; set; }
        public int? AuthorId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool? PublishStatus { get; set; }
        public int? CategoryId { get; set; }

        public virtual AppUser Author { get; set; }
        public virtual Category Category { get; set; }
    }
}
