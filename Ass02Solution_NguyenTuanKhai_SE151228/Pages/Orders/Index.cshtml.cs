using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ass02Solution_NguyenTuanKhai_SE151228.Models;
using Microsoft.AspNetCore.Http;

namespace Ass02Solution_NguyenTuanKhai_SE151228.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext _context;

        public IndexModel(Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; }
        public string msg { get; set; }
        public async Task OnGetAsync()
        {
            
            if (HttpContext.Session.GetString("role").Equals("1"))
            {
                Order = await _context.Orders
                                .Include(o => o.Customer).ToListAsync();
            }
            else
            { 
                var l = await _context.Orders
                                .Include(o => o.Customer).ToListAsync();
                var l1 = from s in _context.Orders.Include(o => o.Customer)
                               select s;
                Order = await l1.Where(o => o.CustomerId == HttpContext.Session.GetInt32("id")).ToListAsync();
            }
            if (HttpContext.Session.GetInt32("id") == null || HttpContext.Session.GetString("role") == null)
            {
                Order = null;
                msg = "Please login to continue use this function, you are not authorized!";
            }
        }
    }
}
