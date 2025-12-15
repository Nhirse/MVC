using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MVC.Models;
using MVC.Models.ViewModels;
using MVC.Services;
using MVC.Data;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthenticateLogin _auth;
        private readonly AppDbContext _context;

        public AccountController(AuthenticateLogin auth, AppDbContext context)
        {
            _auth = auth;
            _context = context;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            var user = _auth.ValidateUser(Username, Password);

            if (user == null)
            {
                return View();
            }

            HttpContext.Session.SetInt32("UserId", user.UserId);
            return RedirectToAction("Home", "Dashboard");
        }

        // GET: /Account/Registration
        [HttpGet]
        public IActionResult Registration()
        {
            return View(new RegisterViewModel());
        }

        // POST: /Account/Create
        [HttpPost]
        public IActionResult Create(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Registration", model);
            }

            // Save later
            return RedirectToAction("Login");
        }
    }
}
