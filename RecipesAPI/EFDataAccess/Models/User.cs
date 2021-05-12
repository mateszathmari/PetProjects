using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace RecipesAPI.Models
{
    public class User
    {
        [Key] [MaxLength(100)] public string UserName { get; set; }
        [Required] [MaxLength(200)] public string Email { get; set; }
        [Required] [MaxLength(300)] public Address Address { get; set; }
        public string Token { get; set; }
        public string HashedPassword { get; set; }
        //public List<Recipe> Recipes { get; set; } = new List<Recipe>();
        public ICollection<UserRecipe> UserRecipes { get; set; } = new HashSet<UserRecipe>();

        public string Salt { get; set; }

        public User()
        {
        }

        public User(string userName, string email, Address address, string password)
        {
            Salt = GenerateUniqueSaltForUser();
            Address = address;
            UserName = userName;
            Email = email;
            HashedPassword = HashingPassword(password);
        }

        private string HashingPassword(string password)
        {
            byte[] salt = Encoding.ASCII.GetBytes(Salt);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public bool IsValidPassword(string password)
        {
            return (HashedPassword == HashingPassword(password));
        }

        public bool ChangePasswordCompleted(string oldPassword, string newPassword)
        {
            if (IsValidPassword(oldPassword))
            {
                this.HashedPassword = HashingPassword(newPassword);
                return true;
            }

            return false;
        }

        private string GenerateUniqueSaltForUser()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public string GenerateToken()
        {
            // generate token without time stamp
            //string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            //generate token with time stamp
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            Token = Convert.ToBase64String(time.Concat(key).ToArray());
            return Token;
        }

        public void DeleteToken()
        {
            this.Token = null;
        }

        private bool HasExpired()
        {
            // validate token expiration lives for 24 hours

            byte[] data = Convert.FromBase64String(Token);
            DateTime now = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            return now < DateTime.UtcNow.AddHours(-24);
        }

        public bool IsValidToken(string givenToken)
        {
            if (Token == null)
            {
                return false;
            }
            else if (HasExpired())
            {
                DeleteToken();
                return false;
            }
            else
            {
                if (givenToken == Token)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}