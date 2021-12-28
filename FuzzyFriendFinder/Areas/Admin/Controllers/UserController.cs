using FuzzyFriendFinder.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*******************************************************************************************
 * Title            :  UserController
 * Description      :  When admin login , they can see Manage drop down in Navbar there are 4 items in drop down ,
 *                     2 of them are Pet Requests, Donations,If we click on either of them 
 *                     that leads to  User Controller.
 *                     User Controller will keep track of Pet Request(adoption requests), Donations. it will 
 *                     taken care by Admin. Only Authorize admins can see 
 *                     Donations list and Pet Requests, it  has two action methods Donations, Interest
 *                     Donations: This action method will retrieve all the records (amount , who donated that amount)
 *                     and display it in particular view this will be in Area/Admin/Views/User/Donations.cshtml
 *                     Interest: This action method will retrieve all the information from Adoption table 
 *                     and Display it in Interest View located in Area/Admin/Views/User/Interest
 * Author           :  Shruthi                     
 *****************************************************************************************/

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
            return View(_db.Donations.Include(m => m.ApplicationUser).OrderByDescending(m=>m.Id).ToList());
        }
        
        public IActionResult Interest()
        {
            return View(_db.Adoptions.Include(m => m.ApplicationUser).OrderByDescending(m=>m.Id).Include(m => m.Pet).ToList());
        }
    }
}
