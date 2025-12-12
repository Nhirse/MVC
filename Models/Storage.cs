using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Storage
    {
        [Key]
        public int FileId { get; set; }
        public int UserId {get; set;}
        public string FileName { get; set; }
        public int numRows { get; set; }
        public int numCol {get; set;}
        public string checksum {get; set;} //making sure things duplicated
        public string dateTime {get; set;}

        public User User {get; set;}
        //why is ther a user here? it creates a user each time it's stored or it just fills in a user to match? Is that necessary when there's userid?
    }

}

          