using Ass03Solution_NguyenTuanKhai_SE151228.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ass03Solution_NguyenTuanKhai_SE151228.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SignalRAssignmentDB03Context _context;
        private readonly IHubContext<SignalrServer> _signalRHub;

        public HomeController(ILogger<HomeController> logger, SignalRAssignmentDB03Context context, IHubContext<SignalrServer> signalRHub)
        {
            _logger = logger;
            _context = context;
            _signalRHub = signalRHub;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("id") != null)  return Redirect("/posts");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create()
        {
            return View();
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,FullName,Address,Password,Email")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                appUser.UserId = _context.AppUsers.Count() + 100;
                _context.Add(appUser);
                await _context.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadAppUsers");
                return RedirectToAction(nameof(Index));
            }
            return View(appUser);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Password,Email")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                var check =  _context.AppUsers.FirstOrDefault(o=>o.Email.ToLower().Equals(appUser.Email.ToLower()) && o.Password.Equals(appUser.Password));
                if (check != null)
                {
                    HttpContext.Session.SetString("name", check.FullName);
                    HttpContext.Session.SetInt32("id", check.UserId);
                    return RedirectToAction("Index");
                }
            }
            return View(appUser);
        }

    }
}
