using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MVC.Models
{
    public class User
    {
        [Key]
        public int UserId {get; set;}
        public string fullName {get; set;}
        public string Username {get; set;}
        public string password {get; set;}
        public List<Storage> Files {get; set;}= new();

    }
}