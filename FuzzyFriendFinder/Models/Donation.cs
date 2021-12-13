using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyFriendFinder.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Required]
        public int Amount { get; set; }


        public string UserId { get; set; }


        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
