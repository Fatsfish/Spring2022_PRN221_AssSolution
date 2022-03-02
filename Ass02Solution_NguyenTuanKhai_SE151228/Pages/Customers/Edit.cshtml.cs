using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ass02Solution_NguyenTuanKhai_SE151228.Models;
using Microsoft.AspNetCore.Http;

namespace Ass02Solution_NguyenTuanKhai_SE151228.Pages.Customers
{
    public class EditModel : PageModel
    {
        private readonly Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext _context;

        public EditModel(Ass02Solution_NguyenTuanKhai_SE151228.Models.PizzaStoreDBAssignmentContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; }
        public Account Account { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);
            Account = await _context.Accounts.FirstOrDefaultAsync(m => m.AccountId == id);

            if (Customer == null && Account ==null)
            {
                return NotFound();
            }

            if(Customer== null && Account != null)
            {
                Customer = new Customer();
                Customer.CustomerId = (int)id;
                Customer.ContactName = Account.FullName;
                Customer.Address = "Fill in to change address";
                Customer.Password = Account.Password;
                Customer.Phone = "0000000000";
                _context.Customers.Add(Customer);
                await _context.SaveChangesAsync();
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

            _context.Attach(Customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.CustomerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            if (HttpContext.Session.GetString("role").Equals("1"))
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
