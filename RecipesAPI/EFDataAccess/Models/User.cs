using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace RecipesAPI.Models
{
    public class User
    {
        private string _userName { get; }
        [Key]
        public int Id { get; set; }
        private string _email { get; }
        private string _address { get; }
        private string _token;

        public User() { }

        public User(string userName, string email, string address)
        {
            _address = address;
            _userName = userName;
            _email = email;
        }

        public string Token
        {
            set
            {
                //token = generateTokenForUser();
            }
        }

        public bool IsCorrectToken(string givenToken)
        {
            return _token == givenToken;
        }
    }
}