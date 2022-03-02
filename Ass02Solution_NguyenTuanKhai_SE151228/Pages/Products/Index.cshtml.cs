using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ass02Solution_NguyenTuanKhai_SE151228.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ass02Solution_NguyenTuanKhai_SE151228.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext _context;

        public IndexModel(Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Category { get; set; }
        public SelectList Suppliers { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Supplier { get; set; }

        public async Task OnGetAsync()
        {
            var genreQuery = from s in _context.Products.Include(o => o.Category)
                     select s.Category.CategoryName;
            var genreQuery1 = from s in _context.Products.Include(o => o.Supplier)
                             select s.Supplier.CompanyName;

            var products = from m in _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(o => o.ProductName.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(Category))
            {
                products = products.Where(x => x.Category.CategoryName.Contains(Category));
            }
            if (!string.IsNullOrEmpty(Supplier))
            {
                products = products.Where(x => x.Supplier.CompanyName.Contains(Supplier));
            }
            Categories = new SelectList(await genreQuery.Distinct().ToListAsync());
            Suppliers = new SelectList(await genreQuery1.Distinct().ToListAsync());
            Product = await products.ToListAsync();
        }
    }
}
