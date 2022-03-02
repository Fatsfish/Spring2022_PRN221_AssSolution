using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ass02Solution_NguyenTuanKhai_SE151228.Models;
using Microsoft.AspNetCore.Http;

namespace Ass02Solution_NguyenTuanKhai_SE151228.Pages.Accounts
{
    public class DeleteModel : PageModel
    {
        private readonly Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext _context;

        public DeleteModel(Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Account Account { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetInt32("id") == null || HttpContext.Session.GetString("role") == null || (HttpContext.Session.GetString("role") != null && HttpContext.Session.GetString("role").Equals("2"))) return RedirectToPage("/Login");

            if (id == null)
            {
                return NotFound();
            }

            Account = await _context.Accounts.FirstOrDefaultAsync(m => m.AccountId == id);

            if (Account == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Account = await _context.Accounts.FindAsync(id);

            if (Account != null)
            {
                _context.Accounts.Remove(Account);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
