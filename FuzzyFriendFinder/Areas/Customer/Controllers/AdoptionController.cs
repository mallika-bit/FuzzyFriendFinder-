using FuzzyFriendFinder.Data;
using FuzzyFriendFinder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

       

        public AdoptionController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        public IActionResult Index(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var adoption = new Adoption();
            var petid = _db.Pets.Where(m => m.Id == id).FirstOrDefault();

            adoption.UserId = claim.Value;
            adoption.PetId = petid.Id;
            adoption.Date = DateTime.Now.Date;
            
        

            _db.Adoptions.Add(adoption);

              _db.SaveChanges();



            //updating pet status
            petid.Status = false;
            _db.SaveChanges();


            HttpContext.Session.SetString("PetRequestMessage", "Thank you for your request. we send SMS to you   ");





            //Send SMS to Customer

            /**  string accountSid = "AC0f3a8bd2534fd3e458ef150e4df35f5d";
              string authToken = "772fdee4710279bf8a3183150947091c";

              TwilioClient.Init(accountSid, authToken);

              var message = MessageResource.Create(
                  body: $"Hello, Thank you for your pet request on Pet Name: {petid.Name},Breed of {petid.Breed} , soon we will contact you ",
                  from: new Twilio.Types.PhoneNumber("+15203919108"),
                  to: new Twilio.Types.PhoneNumber("+12482272837")
              );  ***/  

            return RedirectToAction("Index","Home");
        }
    }
}
