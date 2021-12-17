using FuzzyFriendFinder.Data;
using FuzzyFriendFinder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyFriendFinder.Controllers
{
    //Test Push Branch
    [Area("Customer")]
    public class HomeController : Controller
    {
        //Added Dependency Injection. 

        private readonly ApplicationDbContext _db;
        
        //Added Constructor.
        public HomeController(ApplicationDbContext db)

        {
            _db = db;
        }
        //private readonly ilogger<homecontroller> _logger;

        //public homecontroller(ilogger<homecontroller> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
