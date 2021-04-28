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
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Token> Tokens { get; set; }
 
    }
}

