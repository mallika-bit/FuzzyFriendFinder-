using FuzzyFriendFinder.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuzzyFriendFinder.Models;
using System.Security.Claims;
using Stripe;
using FuzzyFriendFinder.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace FuzzyFriendFinder.Areas.Customer.Controllers
{
    public class DonationController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Donation Donation { get; set; }

        public DonationController(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            return View();
        }


        
        [Area("Customer")]
        
        public IActionResult DonationForm()
        {
            var donation = new Donation();
            return View(donation);
        }


        [Area("Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Payment(string stripeToken)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            Donation.UserId = claim.Value;

            
            _db.Donations.Add(Donation);


            var options = new ChargeCreateOptions
            {
                Amount = Convert.ToInt32(Donation.Amount * 100),
                Currency = "USD",
                Description = "Donation ID : " + Donation.Id,
                Source = stripeToken

            };
            var service = new ChargeService();
            Charge charge = service.Create(options);



            if (charge.Status.ToLower() == "succeeded")
            {
                //var status = SD.PaymentStatusApproved;

                HttpContext.Session.SetString("Message", "Thank you for your Donation. ");
            }
            else
            {
                var stauts = SD.PaymentStatusRejected;
            }


            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
