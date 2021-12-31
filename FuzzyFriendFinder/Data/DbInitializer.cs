﻿using FuzzyFriendFinder.Models;
using FuzzyFriendFinder.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



//Author: Geeta
namespace FuzzyFriendFinder.Data
{
    public class DbInitializer : IDbInitializer
    {

      private readonly ApplicationDbContext _db;
      private readonly UserManager<IdentityUser> _userManager; 
      private readonly RoleManager<IdentityRole> _roleManager;
          
      public DbInitializer(ApplicationDbContext db, UserManager<IdentityUser> userManager,
          RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public void Initialize()
        {
            throw new NotImplementedException();
        }

        public async void initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count()> 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }
            if (_db.Roles.Any(r => r.Name == SD.ManagerUser)) return;

            _roleManager.CreateAsync(new IdentityRole(SD.ManagerUser)).GetAwaiter().GetResult();

            _roleManager.CreateAsync(new IdentityRole(SD.CustomerUser)).GetAwaiter().GetResult();


            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                FirstName = "Geetha Anand",
                EmailConfirmed = true,
                PhoneNumber = "1112223333"
            }, "Admin@123").GetAwaiter().GetResult();

            IdentityUser user = await _db.Users.FirstOrDefaultAsync(u => u.Email == "admin@gmail.com");
            await _userManager.AddToRoleAsync(user, SD.ManagerUser);

        }
    }
}




