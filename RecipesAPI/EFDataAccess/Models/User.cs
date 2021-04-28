using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public Token TokenString { get; set; }
        public string HashedPassword { get; set; }
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();

        public User()
        {
        }

        public User(string userName, string email, Address address, string password)
        {
            Address = address;
            UserName = userName;
            Email = email;
            HashedPassword = HashingPassword(password);
        }

        private string HashingPassword(string password)
        {
            // generate a 128-bit salt using a secure PRNG
            
            string saltString = "SfHfrgd54af4Ghxxc8WxfSSD5y2vdg5dsg6";
            byte[] salt = Encoding.ASCII.GetBytes(saltString);

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
    }
}