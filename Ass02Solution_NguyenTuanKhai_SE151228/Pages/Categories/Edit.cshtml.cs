using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ass02Solution_NguyenTuanKhai_SE151228.Models;

namespace Ass02Solution_NguyenTuanKhai_SE151228.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext _context;

        public EditModel(Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Catergory Catergory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Catergory = await _context.Catergories.FirstOrDefaultAsync(m => m.CategoryId == id);

            if (Catergory == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Catergory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatergoryExists(Catergory.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CatergoryExists(int id)
        {
            return _context.Catergories.Any(e => e.CategoryId == id);
        }
    }
}
