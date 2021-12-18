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
        [StringLength(255)]
        public string Name { get; set; }


        [Required]
        [StringLength(255)]
        public string Description { get; set; }


        [Required]
        public int Weeks { get; set; }

        [Required]
        public int Months { get; set; }


        [Required]
        public int Years { get; set; }


        [Required]
        [StringLength(255)]
        public string Breed { get; set; }



        public bool Status { get; set; }



        [Required]
        [StringLength(50)]
        public string Gender { get; set; }



        [StringLength(50)]
        public string Color { get; set; }

        [Range(4, 100)]
        public int Weight { get; set; }


        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }


        [Required]
        [StringLength(1024)]
        public string ImageUrls { get; set; }

        public Pet()
        {
            Status = true;
        }
    }
}