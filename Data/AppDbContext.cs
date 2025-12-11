using Microsoft.EntityFrameworkCore;
using MVC.Models;


namespace MVC.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        public DbSet<Storage> Storages {get; set;} 
        public DbSet<User> Users {get; set;}
        //Add Tables Here

    }
}
