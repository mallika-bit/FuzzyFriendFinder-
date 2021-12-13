using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyFriendFinder.Models

{             
        public class ApplicationUser : IdentityUser
        {
          
           public string FirstName { get; set; }

           public string LastName { get; set; }
           
           override
          
           public string PhoneNumber { get; set; }
   
            public string City { get; set; }

            public string State { get; set; }
        }
 }

