﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ass02Solution_NguyenTuanKhai_SE151228.Models;

namespace Ass02Solution_NguyenTuanKhai_SE151228.Pages.OrderDetails
{
    public class IndexModel : PageModel
    {
        private readonly Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext _context;

        public IndexModel(Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext context)   
        {
            _context = context;
        }

        public IList<OrderDetail> OrderDetail { get;set; }

        public async Task OnGetAsync()
        {
            OrderDetail = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product).ToListAsync();
        }
    }
}
