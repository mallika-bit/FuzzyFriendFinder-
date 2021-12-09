using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FuzzyFriendFinder.Data
{
    public class ApplicationUser:IdentityUser
    {

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }
    }
}