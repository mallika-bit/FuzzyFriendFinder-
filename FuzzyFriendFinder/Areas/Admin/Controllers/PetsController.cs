﻿using FuzzyFriendFinder.Data;
using FuzzyFriendFinder.Models;
using FuzzyFriendFinder.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace FuzzyFriendFinder.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PetsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PetsController(ApplicationDbContext db)
        {
            _db = db;
        }
        //GET 
        public IActionResult Index()
        {
            return View(_db.Pets.ToListAsync());
        }


        public IActionResult List()
        {
            return View(_db.Pets.Include(x => x.Category).ToList());
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePet(CreatePet createPet)
        {
            //if (_db.Category.ToList().Count() == 0)
            //{
            //    _db.Category.Add(new Category { Name = "Dog" });
            //    _db.Category.Add(new Category { Name = "Cat" });

            //    _db.SaveChanges();
            //}

            //Test@12345

            if (ModelState.IsValid)
            {


                var pet = new Pet
                {
                    Name = createPet.Pet.Name,
                    Status = true,
                    Size = createPet.Pet.Size,
                    Weeks = createPet.Pet.Weeks,
                    Breed = createPet.Pet.Breed,
                    Category = createPet.Pet.Category,
                    Color = createPet.Pet.Color,
                    Description = createPet.Pet.Description,
                    Gender = createPet.Pet.Gender,

                    //ImageUrls = ""
                };

                pet.Category = _db.Categories.ToList().Where(type => type.Id == createPet.Pet.CategoryId).FirstOrDefault();

                //createPet.Pictures.ForEach(picture =>
                //{
                //    var filename = ContentDispositionHeaderValue.Parse(picture.ContentDisposition).FileName.Trim('"');
                //    var uniqueFilename = Guid.NewGuid().ToString() + "_" + filename;
                //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", uniqueFilename);
                //    using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                //    {
                //        picture.CopyTo(stream);
                //    }

                //    pet.ImageUrls += path + uniqueFilename + " ";
                //});

                //if valid
                _db.Pets.Add(pet);

                _db.SaveChanges();

                return RedirectToAction(nameof(List));

            }
            return View(createPet);
        }
    }
}