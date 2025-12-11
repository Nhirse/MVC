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

    }

}

          