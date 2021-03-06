using FuzzyFriendFinder.Data;
using FuzzyFriendFinder.Models;
using FuzzyFriendFinder.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

/**************************************************************************************************************
 * Title            : PetController
 * Description      : When admin login , they can see Manage drop down in Navbar there are 4 items in drop down ,
 *                    2 of them are Create, Manage Pet,If we click on either of them 
 *                    that leads to PetController.
 *                    CreatePet : will take the admin to create Pet form
 *                    ManagePet : where admin can edit Pet and Delete Pet
 *                    
 *                    Create: Action method will leads to Create new pet form.
 *                    CreatePet: Action method will create new Pet ,Store All details in Pet Table
 *                    List: Action method will list all pets with edit and delete button so that admin can 
 *                    edit and delete a pet
 *                    Edit: Action method will leads to edit pet form with pre filled all pet details
 *                    UpdatePet: Action method will update any changes we made to pet
 *                    Delete:  Action mrthod will delete a pet from database
 *                    
 * Author           : Vijaya Laxmi                   
 *************************************************************************************************************/


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
            return View(_db.Pets.Include(x => x.Category).OrderByDescending(m=>m.Id).ToList());
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
            if (createPet.Pet.Years == 0 && createPet.Pet.Weeks == 0 && createPet.Pet.Months == 0)
            {
                ModelState.AddModelError("Pet.Weeks", "Age is required.");
            }

            if (createPet.Pet.CategoryId == 0)
            {
                ModelState.AddModelError("Pet.CategoryId", "Category field is required.");
            }

            if (createPet.Pictures == null)
            {
                ModelState.AddModelError("Pictures", "Atleast one image is required.");
            }

            if (_db.Categories.ToList().Count() == 0)
            {
                _db.Categories.Add(new Category { Name = "Dog" });
                _db.Categories.Add(new Category { Name = "Cat" });

                _db.SaveChanges();
            }

            createPet.Pet.ImageUrls = "Sample";


            if (ModelState.IsValid)
            {


                var pet = new Pet
                {
                    Name = createPet.Pet.Name,
                    Status = true,
                    Weight = createPet.Pet.Weight,
                    Weeks = createPet.Pet.Weeks,
                    Years = createPet.Pet.Years,
                    Months = createPet.Pet.Months,
                    Breed = createPet.Pet.Breed,
                    Category = createPet.Pet.Category,
                    Color = createPet.Pet.Color,
                    Description = createPet.Pet.Description,
                    Gender = createPet.Pet.Gender,
                };

                pet.Category = _db.Categories.ToList().Where(type => type.Id == createPet.Pet.CategoryId).FirstOrDefault();

                createPet.Pictures.ForEach(picture =>
                {
                    var filename = ContentDispositionHeaderValue.Parse(picture.ContentDisposition).FileName.Trim('"');
                    var uniqueFilename = Guid.NewGuid().ToString() + filename.Substring(filename.Length - 4);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", uniqueFilename);
                    using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                    {
                        picture.CopyTo(stream);
                    }

                    var pathToSave = "/images/" + uniqueFilename;

                    if (picture.FileName != createPet.Pictures.Last().FileName)
                    {
                        pet.ImageUrls += pathToSave + ", ";
                    }
                    else
                    {
                        pet.ImageUrls += pathToSave;
                    }

                });

                _db.Pets.Add(pet);

                _db.SaveChanges();

                return RedirectToAction(nameof(List));

            }
            else
            {
                return View("Create");
            }

        }



        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var pet = _db.Pets.Where(pet => pet.Id == id).FirstOrDefault();

            if (pet == null)
            {
                return NotFound();
            }

            var editPet = new EditPet();
            editPet.Pet = pet;
            editPet.Id = pet.Id;
            editPet.ExistingImageUrls = pet.ImageUrls;

            return View(editPet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePet(EditPet editPet)
        {
            if (editPet.Pet.Years == 0 && editPet.Pet.Weeks == 0 && editPet.Pet.Months == 0)
            {
                ModelState.AddModelError("Pet.Weeks", "Age is required.");
            }

            if (editPet.Pet.CategoryId == 0)
            {
                ModelState.AddModelError("Pet.CategoryId", "Category field is required.");
            }

            if (editPet.Pictures == null && editPet.ExistingImageUrls == null)
            {
                ModelState.AddModelError("Pictures", "Atleast one image is required.");
            }

            if (ModelState.IsValid)
            {

                var petToUpdate = _db.Pets.Where(pet => pet.Id == editPet.Id).FirstOrDefault();

                petToUpdate.Breed = editPet.Pet.Breed;
                petToUpdate.Category = _db.Categories.Where(category => category.Id == editPet.Pet.CategoryId).FirstOrDefault();
                petToUpdate.CategoryId = editPet.Pet.CategoryId;
                petToUpdate.Color = editPet.Pet.Color;
                petToUpdate.Description = editPet.Pet.Description;
                petToUpdate.Gender = editPet.Pet.Gender;
                petToUpdate.Months = editPet.Pet.Months;
                petToUpdate.Name = editPet.Pet.Name;
                petToUpdate.Status = editPet.Pet.Status;
                petToUpdate.Weeks = editPet.Pet.Weeks;
                petToUpdate.Weight = editPet.Pet.Weight;
                petToUpdate.Years = editPet.Pet.Years;


                var existingImageUrls = editPet.ExistingImageUrls;

                petToUpdate.ImageUrls = "";

                if (string.IsNullOrEmpty(existingImageUrls))
                {
                    if (editPet.Pictures == null)
                    {
                        ModelState.AddModelError("Pictures", "Atleast one image is required.");
                        return View("Edit", editPet);
                    }
                    else
                    {
                        editPet.Pictures.ForEach(picture =>
                        {
                            var filename = ContentDispositionHeaderValue.Parse(picture.ContentDisposition).FileName.Trim('"');
                            var uniqueFilename = Guid.NewGuid().ToString() + filename.Substring(filename.Length - 4);
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", uniqueFilename);
                            using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                            {
                                picture.CopyTo(stream);
                            }

                            var pathToSave = "/images/" + uniqueFilename;

                            if (picture.FileName != editPet.Pictures.Last().FileName)
                            {
                                petToUpdate.ImageUrls += pathToSave + ", ";
                            }
                            else
                            {
                                petToUpdate.ImageUrls += pathToSave;
                            }

                        });

                    }
                }
                else
                {
                    if (editPet.Pictures == null)
                    {
                        //directly update the database with what is in createPet.Pet.ImageUrls
                        existingImageUrls = existingImageUrls.Trim();
                        while (existingImageUrls.EndsWith(',') || existingImageUrls.EndsWith(' '))
                        {
                            existingImageUrls = existingImageUrls.Substring(0, existingImageUrls.Length - 1);
                        }
                        petToUpdate.ImageUrls = existingImageUrls;
                    }
                    else
                    {
                        //combine existing image urls with new image urls and update database.

                        editPet.Pictures.ForEach(picture =>
                        {
                            var filename = ContentDispositionHeaderValue.Parse(picture.ContentDisposition).FileName.Trim('"');
                            var uniqueFilename = Guid.NewGuid().ToString() + filename.Substring(filename.Length - 4);
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", uniqueFilename);
                            using (System.IO.Stream stream = new FileStream(path, FileMode.Create))
                            {
                                picture.CopyTo(stream);
                            }

                            var pathToSave = "/images/" + uniqueFilename;

                            if (picture.FileName != editPet.Pictures.Last().FileName)
                            {
                                petToUpdate.ImageUrls += pathToSave + ", ";
                            }
                            else
                            {
                                petToUpdate.ImageUrls += pathToSave;
                            }

                        });

                        petToUpdate.ImageUrls += ", " + existingImageUrls;

                    }
                }

                _db.SaveChanges();
                return RedirectToAction(nameof(List));
            }
            else
            {
                return View("Edit", editPet);
            }

        }

        // GET: Student/Delete/5
        public ActionResult Delete(int id)
        {
            _db.Pets.Remove(_db.Pets.Where(pet => pet.Id == id).FirstOrDefault());

            _db.SaveChanges();

            return RedirectToAction("List");
        }
    }
}
