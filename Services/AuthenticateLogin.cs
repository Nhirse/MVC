using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Data;

namespace MVC.Services
{
    public class AuthenticateLogin
    {
        private readonly AppDbContext _context;
        public AuthenticateLogin(AppDbContext context)
        {
            _context = context;
        }

        public User? ValidateUser(string username, string password)
        {
            return _context.Users
                .FirstOrDefault(u => (u.Username == username) && (u.Password == password));
        }
    }
}
        //take in the paramater that's the username and password posted by Login.cshtml view
        //parse the tables to find userID that matches
        //make a copy of the user, assign to variable user
        //return copy of user
        
    

