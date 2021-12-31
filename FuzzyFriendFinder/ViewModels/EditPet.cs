using FuzzyFriendFinder.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyFriendFinder.ViewModels
{
    public class EditPet
    {
        public Pet Pet { get; set; }
        public List<IFormFile> Pictures { get; set; }       
        public int Id { get; set; }
        public string ExistingImageUrls { get; set; }
    }
}
