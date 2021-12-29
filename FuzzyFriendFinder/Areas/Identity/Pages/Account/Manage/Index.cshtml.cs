using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FuzzyFriendFinder.Models;
using FuzzyFriendFinder.Data;
using Microsoft.EntityFrameworkCore;

namespace FuzzyFriendFinder.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        //private readonly ApplicationUser applicationUser;

        private readonly ApplicationDbContext _db;

        
        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            public string FirstName { get; set; }

            [Required]
            public string Email { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            public string Address { get; set; }

            [Required]
            public string City { get; set; }

            [Required]
            public string State { get; set; }

            [Required]
            public string Zipcode { get; set; }

        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var userId = user.Id;
            var appUser = await _db.ApplicationUsers
                .Where(x => x.Id == userId).FirstOrDefaultAsync();


            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Email = email,
                FirstName = ((ApplicationUser)user).FirstName,
                LastName = ((ApplicationUser)user).LastName,
                Address = appUser.Address,
                City = appUser.City,
                State = appUser.State,
                Zipcode = appUser.Zipcode

            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            var appUser = await _db.ApplicationUsers
                .Where(x => x.Id == userId).FirstOrDefaultAsync();

            appUser.FirstName = Input.FirstName;
            appUser.LastName = Input.LastName;
            appUser.Email = Input.Email;
            appUser.PhoneNumber = Input.PhoneNumber;
            appUser.Address = Input.Address;
            appUser.City = Input.City;
            appUser.State = Input.State;
            appUser.Zipcode = Input.Zipcode;

            _db.Update(appUser).State = EntityState.Modified;

            await _db.SaveChangesAsync();


            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
