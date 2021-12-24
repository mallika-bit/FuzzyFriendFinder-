using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

/***********************************************************************
 * Author: Mallika
 ***********************************************************************/


namespace FuzzyFriendFinder.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than ${0}")]
        public int Amount { get; set; }


        public string UserId { get; set; }


        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
