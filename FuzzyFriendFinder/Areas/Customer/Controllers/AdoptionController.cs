using FuzzyFriendFinder.Data;
using FuzzyFriendFinder.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace FuzzyFriendFinder.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class AdoptionController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Adoption adoption { get; set; }

        public AdoptionController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            // var adoption = new Adoption();
            var petid = _db.Pets.Where(m => m.Id == id).FirstOrDefault();

            adoption.UserId = claim.Value;
            adoption.PetId = petid.Id;
            adoption.Date = DateTime.Now.Date;
            
        

            _db.Adoptions.Add(adoption);

              _db.SaveChanges();

            //Send SMS to Customer

            string accountSid = "AC0f3a8bd2534fd3e458ef150e4df35f5d";
            string authToken = "134a97ae064efb81ba1e1ab5893da3d4";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: "Hello, Thank you for your request, soon we will contact you ",
                from: new Twilio.Types.PhoneNumber("+15203919108"),
                to: new Twilio.Types.PhoneNumber("+12482272837")
            );

            return RedirectToAction("Index","Home");
        }
    }
}
