using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ass02Solution_NguyenTuanKhai_SE151228.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Ass02Solution_NguyenTuanKhai_SE151228.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext _context;
        private IHostingEnvironment _environment;
        public List<String> Files { get; set; }

        public CreateModel(Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("id") == null || HttpContext.Session.GetString("role") == null || (HttpContext.Session.GetString("role") != null && HttpContext.Session.GetString("role").Equals("2"))) return RedirectToPage("/Login");

            ViewData["CategoryId"] = new SelectList(_context.Catergories, "CategoryId", "CategoryName");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "Address");
            //Fetch all files in the Folder (Directory).
            string[] filePaths = Directory.GetFiles(Path.Combine(_environment.WebRootPath, "Images"));
            //Copy File names to Model collection.
            this.Files = new List<String>();
            foreach (string filePath in filePaths)
            {
                if (Path.GetFileName(filePath).ToLower().Contains("png") || Path.GetFileName(filePath).ToLower().Contains(".jpg") || Path.GetFileName(filePath).ToLower().Contains(".jpeg") || Path.GetFileName(filePath).ToLower().Contains(".gif"))
                {
                    this.Files.Add(Path.GetFileName(filePath));
                }
            }
            ViewData["Image"] = new SelectList(Files);
            return Page();
        }


        [BindProperty]
        public Product Product { get; set; }

 
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
           
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
