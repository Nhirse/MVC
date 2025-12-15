using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models.ViewModels
{
    public class DashboardViewModel
    {
        public string fullName {get; set;}
        public int TotalFiles {get; set;}
        public DateTime? LastUploaded {get; set;}

    }
}