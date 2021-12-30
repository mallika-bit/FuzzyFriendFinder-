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

/******************************************************************************************************
 * Title                : AdoptionController
 * Desciprion           : When someone clicks on Interest Button(Areas/Customer/Views/Home/Details.cshtml) which
 *                        is in Details View , Which means customer shows Interest in adopting particular Pet,
 *                        and request for it, This controller will do 4 things 
 *                        1.Store the request Date along with customer Id, pet Id in Adoption database.
 *                        2.Change the status of pet from available to unavailable (so in database pet status 
 *                          will be false from true.
 *                        3.Send SMS to Customer that saying soon we will contact them
 *                        4.Displying message in Index Home page that Thank you for your request
 *                        
 *                        For SMS we installed twilio package in our project
 *                        we used Auth Token , AccountSid which Twilio party provided
 *                        we will email Auth Token , right now we commented out that section of code, 
 *                        To test SMS please remove commented line 83 and 92, use New Auth Token which we 
 *                        emailed.
 *                        This is done by In guidence of Mentor Ravish 
 * Author               : Mallika                        
 * 
 ***************************************************************************************************/




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


            HttpContext.Session.SetString("PetRequestMessage", "Thank you for your interest!You’ll receive a SMS confirmation soon.We’ll be in touch about the adoption process. ");





            //Send SMS to Customer

            string accountSid = "AC0f3a8bd2534fd3e458ef150e4df35f5d";
            string authToken = "103fd28bdd418820ee49de0d5d83f2c6";

            //get the phoneNumber from database
            string phoneNumber = _db.Users.FirstOrDefault(u => u.Id == claim.Value).PhoneNumber;
            

            TwilioClient.Init(accountSid, authToken);

              var message = MessageResource.Create(
                  body: $"Hello, Thank you for your pet request on Pet Name: {petid.Name},Breed of {petid.Breed} , soon we will contact you ",
                  from: new Twilio.Types.PhoneNumber("+15203919108"),
                  to: new Twilio.Types.PhoneNumber(phoneNumber)
              );  

            return RedirectToAction("Index","Home");
        }
    }
}
