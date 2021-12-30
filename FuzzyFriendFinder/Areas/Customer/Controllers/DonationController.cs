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

/**************************************************************************************************
 * Title            :  DonationController
 * Description      :  Donation Button is in Area/Customer/Views/Home/GetInvolved View,Donation Button 
 *                     allows customer to make donations for FuzzyFriendFinder website
 *                     When donation button clicked on that takes customer to donation form(which is in 
 *                     Area/Customer/Views/Donation/Index where they can 
 *                     enter amount and make payment through stripe credit card transaction.
 *                     This is done by based on the reference of spice project in udemy , where it uses stripe for 
 *                     credit card transaction.
 *                     
 *                     Inorder to work with stripe we installed stripe.net package in our project and 
 *                     we configured stripe settings in startup.cs file
 *                     
 *                     It has Two action methods
 *                     Index - will take customer to  donation form
 *                     Payment - will take care of payment processing and also store Customer name and Donation
 *                     amount in Donations Database, And display the thank you message in home index page
 *                     when donation is done.
 *                     Customer should be authorized to make donations.
 *                     
 *                     This controller will also do store the amount , user details in database.so that
 *                     admin can track the records of who made donations and how much amount they donated
 *                     
 * Author           :  Mallika          
 * 
 * 
 *******************************************************************************************/




namespace FuzzyFriendFinder.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
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
            var donation = new Donation();
            return View(donation);
           
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(string stripeToken)
        {
            //Get the customer who logged in
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            Donation.UserId = claim.Value;

            
            _db.Donations.Add(Donation);


            //payment process
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
