using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyFriendFinder.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PetController : Controller
    {
        /// <summary>
        /// /
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
