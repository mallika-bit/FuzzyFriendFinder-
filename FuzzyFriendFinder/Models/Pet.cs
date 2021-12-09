using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyFriendFinder.Models
{
    public class Pet
    {

        [Required]
        public int Id { get; set; }

        [Required]
        public string  Name { get; set; }


        [Required]
        public string  Description { get; set; }


        [Required]
        public int Age { get; set; }


        [Required]
        public string Breed { get; set; }


        
        public bool  Status { get; set; }

        [Required]
        public string Gender { get; set; }

        public string Color { get; set; }

        public int Size { get; set; }

        
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public Pet()
        {
            Status = true;
        }
    }
}
