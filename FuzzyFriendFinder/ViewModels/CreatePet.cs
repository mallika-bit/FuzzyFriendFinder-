using FuzzyFriendFinder.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyFriendFinder.ViewModels
{
    public class CreatePet
    {
        public Pet Pet { get; set; }
        public List<IFormFile> Pictures { get; set; } 
    }
}
