using Microsoft.EntityFrameworkCore;
using RecipesAPI.Models;

namespace EFDataAccess.DataAccess
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
 
    }
}

