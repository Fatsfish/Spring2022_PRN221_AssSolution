using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ass02Solution_NguyenTuanKhai_SE151228.Models;

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

        public async Task OnGetAsync()
        {
            Order = await _context.Orders
                .Include(o => o.Customer).ToListAsync();
        }
    }
}
