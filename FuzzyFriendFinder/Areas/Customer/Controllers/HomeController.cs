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


/******************************************************************************************************************************************
 * Title:          Home Controller
 * Description:    Home controller has Action methods related to links in  home page and navbar ,It has
 *                 Index ,About -Display Respective Views And Hard coded HTML ,CSS
 *                 GetInvolved contains Donation Button, for this button  action method is in 'DonationController'.
 *                 
 *                 (Here available means status=true)
 *                 Cats: This action method will retrieve All available cats from database and display in Cats View.
 *                 Dogs: This action method will retrieve All available dogs from database and display in dogs View 
 *                 AllListings: This action method will  retrieve All pets including available and unavailable from database , display in AllListings View
 *                 
 *                 Details: This action method will retrieve all  details about particular pet, when we clicked on details link in Alllistings,Dogs,Cats views  
 *                          Details View has Interest Button ,When someone clicks on it it will go to 'AdoptionController'
 *                          
 *                 Search: This action method will search based on pet name, breed, color.Display results in SearchPageListing View
 *                 
 *                 Register: Register new user //these views can see in Identity/pages/register.cshtml
 *                 Login: User can login       // Identity/pages/login.cshtml
 *                 
 *                 Admin: For common admin creation DbInitialzer/IDbInitializer class is in Data folder
 *                 
 *                 ViewComponents: Used to display user name in navbar , it is located in 
 *                                 ViewComponents/UserNameViewComponent.cs, View file is in 
 *                                 Views/Shared/Components/UserName/Default.cshtml
 *                 
 *                 
 * HomeController
 *       Author        :   Prasita
 * Views Author
 * Index,Layout,
 * About,GetInvolved 
 * Logo                :   JulesKFisher, Amelia
 * Cats,Dogs,
 * AllListings         :   Prasita
 * 
 * DetailsPage/
 * Displaying
 * Username instead 
 * of email in Navbar  :   Mallika
 * 
 * Serach              :   Geetha
 * Register/Login/
 * AdminCreation       :   Geetha
 *******************************************************************************************************************************************/




namespace FuzzyFriendFinder.Controllers
{
    
    [Area("Customer")]
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)

        {
            _db = db;
        }


        public IActionResult Index()
        {
            

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult GetInvolved()
        {
            return View();
        }


        public async Task<IActionResult> Cats()
        {
            var cats = await _db.Pets.Where(x => x.Category.Id == 2).Where(m => m.Status == true).OrderByDescending(m=>m.Id).ToListAsync();

            ViewBag.cats = cats;

            return View();
        }

        public async Task<IActionResult> Dogs()
        {
            var dogs = await _db.Pets.Where(x => x.Category.Id == 1).Where(m => m.Status == true).OrderByDescending(m=>m.Id).ToListAsync();

            ViewBag.dogs = dogs;

            return View();
        }

        public async Task<IActionResult> AllListings()
        {
            var allListings = await _db.Pets.OrderByDescending(m=>m.Id).ToListAsync();

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
