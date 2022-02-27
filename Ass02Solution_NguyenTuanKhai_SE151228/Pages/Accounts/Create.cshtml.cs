using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ass02Solution_NguyenTuanKhai_SE151228.Models;

namespace Ass02Solution_NguyenTuanKhai_SE151228.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext _context;

        public CreateModel(Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Accounts.Add(Account);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
