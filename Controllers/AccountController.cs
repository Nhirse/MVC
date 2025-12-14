using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;
using MVC.Data;

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

    }
}
