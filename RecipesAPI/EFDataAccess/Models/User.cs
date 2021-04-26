using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace RecipesAPI.Models
{
    public class User
    {
        
        [Key]
        public int Id { get; set; }
        [Required] [MaxLength(100)]
        public string UserName { get; }
        [Required]
        [MaxLength(200)]
        public string Email { get; set; }
        [Required]
        [MaxLength(300)]
        public Address Address { get; set; }
        public Token TokenString { get; set; }
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();

        public User() { }

        public User(string userName, string email, Address address)
        {
            Address = address;
            UserName = userName;
            Email = email;
        }


        public bool IsCorrectToken(Token givenToken)
        {
            return TokenString == givenToken;
        }
    }
}