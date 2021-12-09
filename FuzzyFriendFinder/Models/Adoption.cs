using FuzzyFriendFinder.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyFriendFinder.Models
{
    public class Adoption
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int PetId { get; set; }


        [ForeignKey("PetId")]
        public virtual Pet Pet { get; set; }

        public string UserId { get; set; }


        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }


    }
}
