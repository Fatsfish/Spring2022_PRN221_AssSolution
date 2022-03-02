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
    public class DetailsModel : PageModel
    {
        private readonly Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext _context;

        public DetailsModel(Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext context)
        {
            _context = context;
        }

        public Order Order { get; set; }
        public IList<OrderDetail> OrderDetail { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetInt32("id") == null || HttpContext.Session.GetString("role") == null || (HttpContext.Session.GetString("role") != null && HttpContext.Session.GetString("role").Equals("2"))) return RedirectToPage("/Login");

            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Orders
                .Include(o => o.Customer).FirstOrDefaultAsync(m => m.OrderId == id);

            if (Order == null)
            {
                return NotFound();
            }

            var l1 = from s in _context.OrderDetails.Include(o => o.Order).Include(o=>o.Product)
                     select s;
            OrderDetail = await l1.Where(o => o.OrderId == id).ToListAsync();

            return Page();

        }
    }
}
