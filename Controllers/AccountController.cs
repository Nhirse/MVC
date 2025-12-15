using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;
using MVC.Data;
using MVC.Models.ViewModels;
using BCrypt.Net;


namespace MVC.Controllers
{
    public class AccountController: Controller
    {
        private readonly AuthenticateLogin _auth;
        private readonly AppDbContext _context;

        public AccountController(AuthenticateLogin auth, AppDbContext context)
        {
            _auth = auth;
            _context=context;
        }
        [HttpPost]    
        public IActionResult Login(string Username, string Password)
        {
            var user = _auth.ValidateUser(Username, Password);

            if (user == null)
            {
                TempData["IncorrectPassword"] = "Incorrect Username or Password. Try Again.";
            
            return View();
            }
            HttpContext.Session.SetInt32("UserId", user.UserId);

            return RedirectToAction ("Home", "Dashboard");

            //redirect to 
            // Dashboard.home()
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Registration(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool usernameExists = _context.Users.Any(u => u.Username == model.Username);
            if (usernameExists)
            {
                ModelState.AddModelError("", "Username already exists");
                return View(model);
            }

            var user = new User
            {
                fullName = model.FullName,
                Username = model.Username,
                Email = model.Email,
                Phone = model.Phone,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Account created successfully. Please log in.";
            return RedirectToAction("Login");
        }


    }
}
