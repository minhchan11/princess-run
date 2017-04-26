using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using princessrun.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace princessrun.Controllers
{
    public class ThingsController : Controller
    {
        private readonly ApplicationDbContext _db;
        // GET: /<controller>/
        public ThingsController()
        {
         
        }
        public ThingsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _db.Things.ToListAsync();
            return View(list);
        }
    }
}
