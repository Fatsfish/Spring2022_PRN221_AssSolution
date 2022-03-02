using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ass02Solution_NguyenTuanKhai_SE151228.Models;
using Microsoft.AspNetCore.Http;

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
        public string msg { get; set; }
        public async Task OnGetAsync()
        {
            OrderDetail = await _context.OrderDetails
                .Include(o => o.Order)
                .Include(o => o.Product).ToListAsync();
            if (HttpContext.Session.GetInt32("id") == null || HttpContext.Session.GetString("role") == null || (HttpContext.Session.GetString("role") != null && HttpContext.Session.GetString("role").Equals("2")))
            {
                OrderDetail = null;
                msg = "Please login to continue use this function, you are not authorized!";
            }
        }
    }
}
