using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ass03Solution_NguyenTuanKhai_SE151228.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;

namespace Ass03Solution_NguyenTuanKhai_SE151228.Controllers
{
    public class PostCategoriesController : Controller
    {
        private readonly SignalRAssignmentDB03Context _context;
        private readonly IHubContext<SignalrServer> _signalRHub;

        public PostCategoriesController(SignalRAssignmentDB03Context context, IHubContext<SignalrServer> signalRHub)
        {
            _context = context;
            _signalRHub = signalRHub;
        }

        // GET: PostCategories
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }
            return View(await _context.PostCategories.ToListAsync());
        }

        [HttpGet]
        public IActionResult GetPostCates()
        {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }
            var res = _context.PostCategories.ToList();
            return Ok(res);
        }
        // GET: PostCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }

            if (id == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (postCategory == null)
            {
                return NotFound();
            }

            return View(postCategory);
        }

        // GET: PostCategories/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }

            return View();
        }

        // POST: PostCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Description")] PostCategory postCategory)
        {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }

            if (ModelState.IsValid)
            {
                _context.Add(postCategory);
                await _context.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadPostCategories");
                return RedirectToAction(nameof(Index));
            }
            return View(postCategory);
        }

        // GET: PostCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }

            if (id == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategories.FindAsync(id);
            if (postCategory == null)
            {
                return NotFound();
            }
            return View(postCategory);
        }

        // POST: PostCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,Description")] PostCategory postCategory)
        {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }

            if (id != postCategory.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postCategory);
                    await _context.SaveChangesAsync();
                    await _signalRHub.Clients.All.SendAsync("LoadPostCategories");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostCategoryExists(postCategory.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(postCategory);
        }

        // GET: PostCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }

            if (id == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (postCategory == null)
            {
                return NotFound();
            }

            return View(postCategory);
        }

        // POST: PostCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }

            var postCategory = await _context.PostCategories.FindAsync(id);
            _context.PostCategories.Remove(postCategory);
            await _context.SaveChangesAsync();
            await _signalRHub.Clients.All.SendAsync("LoadPostCategories");
            return RedirectToAction(nameof(Index));
        }

        private bool PostCategoryExists(int id)
        {
            return _context.PostCategories.Any(e => e.CategoryId == id);
        }
    }
}
