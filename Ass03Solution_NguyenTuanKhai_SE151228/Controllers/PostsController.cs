using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ass03Solution_NguyenTuanKhai_SE151228.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace Ass03Solution_NguyenTuanKhai_SE151228.Controllers
{
    public class PostsController : Controller
    {
        private readonly SignalRAssignmentDB03Context _context;
        private readonly IHubContext<SignalrServer> _signalRHub;
        private readonly IConfiguration Configuration;

        public PostsController(SignalRAssignmentDB03Context context, IHubContext<SignalrServer> signalRHub, IConfiguration configuration)
        {
            _context = context;
            _signalRHub = signalRHub;
            Configuration = configuration;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }

            var signalRAssignmentDB03Context = _context.Posts.Include(p => p.Author).Include(p => p.Category);
            return View(await signalRAssignmentDB03Context.ToListAsync());
        }

        [HttpGet]
        public IActionResult GetPosts() {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }

            var res = _context.Posts.ToList();
            return Ok(res);
        }
        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }

            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }

            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "UserId", "UserId");
            ViewData["CategoryId"] = new SelectList(_context.PostCategories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,CreatedDate,UpdatedDate,Title,Content,PublishStatus,AuthorId,CategoryId")] Post post)
        {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }

            if (ModelState.IsValid)
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadPosts");
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "UserId", "UserId", post.AuthorId);
            ViewData["CategoryId"] = new SelectList(_context.PostCategories, "CategoryId", "CategoryName", post.CategoryId);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }

            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "UserId", "UserId", post.AuthorId);
            ViewData["CategoryId"] = new SelectList(_context.PostCategories, "CategoryId", "CategoryName", post.CategoryId);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,CreatedDate,UpdatedDate,Title,Content,PublishStatus,AuthorId,CategoryId")] Post post)
        {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }

            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                    await _signalRHub.Clients.All.SendAsync("LoadPosts");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostId))
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
            ViewData["AuthorId"] = new SelectList(_context.AppUsers, "UserId", "UserId", post.AuthorId);
            ViewData["CategoryId"] = new SelectList(_context.PostCategories, "CategoryId", "CategoryName", post.CategoryId);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }

            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.Author)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetInt32("id") == null) { HttpContext.Session.SetString("error", "Please login first to access!"); return Redirect("/"); }

            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            await _signalRHub.Clients.All.SendAsync("LoadPosts");
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.PostId == id);
        }
    }
}
