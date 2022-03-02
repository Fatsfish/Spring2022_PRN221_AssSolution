using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ass02Solution_NguyenTuanKhai_SE151228.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ass02Solution_NguyenTuanKhai_SE151228.Pages
{
    public class ReportModel : PageModel
    {
        private readonly Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext _context;

        public ReportModel(Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext context)
        {
            _context = context;
        }

        public IList<OrderDetail> OrderDetail { get; set; }
        public string msg { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime a { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime b { get; set; }
        public double total { get; set; }
        public async Task OnGetAsync()
        {
            var list = from m in _context.OrderDetails
                .Include(p => p.Order)
                .Include(p => p.Product)
                           select m;
            if (a!=DateTime.MinValue && b!=DateTime.MinValue)
            {
                if(a>b)
                list = list.Where(o => o.Order.OrderDate >= a && o.Order.OrderDate <= b);
                if(b>a)
                list = list.Where(o => o.Order.OrderDate >= a && o.Order.OrderDate <= b);
                if(b==a)
                list = list.Where(o => o.Order.OrderDate == a);
                list = list.OrderByDescending(o => o.Order.OrderDate);
                OrderDetail = await list.ToListAsync();
            }
            if (OrderDetail != null)
            {
                foreach (var x in OrderDetail)
                {
                    total += x.Quantity * (double)x.UnitPrice;
                }
            }
            if (HttpContext.Session.GetInt32("id") == null || HttpContext.Session.GetString("role") == null || (HttpContext.Session.GetString("role") != null && HttpContext.Session.GetString("role").Equals("2")))
            {
                OrderDetail = null;
                msg = "Please login to continue use this function, you are not authorized!";
            }
        }
    }
}
