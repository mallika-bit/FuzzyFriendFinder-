using FuzzyFriendFinder.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyFriendFinder.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Donations()
        {
            return View(_db.Donations.Include(m => m.ApplicationUser).ToList());
        }
    }
}
