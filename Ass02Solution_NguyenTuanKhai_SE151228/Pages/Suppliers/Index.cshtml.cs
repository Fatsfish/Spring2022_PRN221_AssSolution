using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ass02Solution_NguyenTuanKhai_SE151228.Models;
using Microsoft.AspNetCore.Http;

namespace Ass02Solution_NguyenTuanKhai_SE151228.Pages.Suppliers
{
    public class IndexModel : PageModel
    {
        private readonly Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext _context;

        public IndexModel(Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext context)
        {
            _context = context;
        }

        public IList<Supplier> Supplier { get;set; }
        public string msg { get; set; }
        public async Task OnGetAsync()
        {
            Supplier = await _context.Suppliers.ToListAsync();
            if (HttpContext.Session.GetInt32("id") == null || HttpContext.Session.GetString("role") == null || (HttpContext.Session.GetString("role") != null && HttpContext.Session.GetString("role").Equals("2")))
            {
                Supplier = null;
                msg = "Please login to continue use this function, you are not authorized!";
            }
        }
    }
}
