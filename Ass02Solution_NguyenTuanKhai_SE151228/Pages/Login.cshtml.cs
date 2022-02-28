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
    public class LoginModel : PageModel
    {
        private readonly Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext _context;

        public LoginModel(Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("role");
            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("username");
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; }
        public String msg;
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (string.IsNullOrEmpty(Account.UserName) || string.IsNullOrEmpty(Account.Password))
            {
                msg = "Please enter your username and password and try again.";
                return Page();
            }
            else
            {
                Account = await _context.Accounts.FirstOrDefaultAsync(m => m.UserName == Account.UserName && m.Password == Account.Password);
                if (Account == null)
                {
                    msg = "The account doesn't exist!";
                    return Page();
                }
                HttpContext.Session.SetString("role", Account.Type);
                HttpContext.Session.SetInt32("id", Account.AccountId);
                HttpContext.Session.SetString("username", Account.UserName);
                
            }
            if (Account.Type.Equals("1"))
            {
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage("Index");
            }
        }
    }
}
