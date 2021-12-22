using FuzzyFriendFinder.Data;
using FuzzyFriendFinder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)

        {
            _db = db;
        }


        public async Task<IActionResult> Index()
        {
            var pet = await _db.Pets.Where(m => m.Status == true).ToListAsync();

            return View(pet);
        }

        public IActionResult About()
        {
            return View();
        }


        public async Task<IActionResult> Cats()
        {
            var cats = await _db.Pets.Where(x => x.Category.Id == 2).Where(m => m.Status == true).ToListAsync();

            ViewBag.cats = cats;

            return View();
        }

        public async Task<IActionResult> Dogs()
        {
            var dogs = await _db.Pets.Where(x => x.Category.Id == 1).Where(m => m.Status == true).ToListAsync();

            ViewBag.dogs = dogs;

            return View();
        }

        public async Task<IActionResult> AllListings()
        {
                 var allListings = await _db.Pets.ToListAsync();

                return View(allListings);
        }
        public async Task<IActionResult> SearchPageListing(String SearchString)
        {

            var petNameSearch = from m in _db.Pets select m;
            var petColorSearch = from m in _db.Pets select m;
            var petBreedSearch = from m in _db.Pets select m;


            if (!String.IsNullOrEmpty(SearchString))
            {
                petNameSearch = petNameSearch.Where(s => s.Name.Contains(SearchString));
                if (petNameSearch.Count() > 0)
                {
                    return View(await petNameSearch.ToListAsync());
                }
                
                petColorSearch = petColorSearch.Where(s => s.Color.Contains(SearchString));
                if (petColorSearch.Count() > 0)
                {
                    return View(await petColorSearch.ToListAsync());
                }

                petBreedSearch = petBreedSearch.Where(s => s.Breed.Contains(SearchString));
                if (petBreedSearch.Count() > 0)
                {
                    return View(await petBreedSearch.ToListAsync());
                }
            }
            return View();
        }


           public IActionResult Privacy()
            {
                 return View();
            }



        public async Task<IActionResult> Details(int id)
        {
            var petdet = await _db.Pets.Include(m => m.Category).Where(m => m.Id == id).FirstOrDefaultAsync();

            return View(petdet);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
