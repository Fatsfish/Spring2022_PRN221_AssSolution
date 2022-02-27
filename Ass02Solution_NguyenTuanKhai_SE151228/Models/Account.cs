using System;
using System.Collections.Generic;

#nullable disable

namespace Ass02Solution_NguyenTuanKhai_SE151228.Models
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Type { get; set; }
    }
}
