using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RecipesAPI.Models
{
    public class Token
    {
        [Key] public int Id { get; set; }
        [Required] [MaxLength(50)] public string TokenString { get; set; }

        public Token()
        {
            // generate token without time stamp
            //string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            //generate token with time stamp
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            TokenString = Convert.ToBase64String(time.Concat(key).ToArray());
        }

        private bool HasExpired()
        {
            // validate token expiration lives for 24 hours

            byte[] data = Convert.FromBase64String(TokenString);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            return when < DateTime.UtcNow.AddHours(-24);
        }

        public bool IsValidToken(string givenToken)
        {
            if (TokenString == null)
            {
                return false;
            }
            else if (HasExpired())
            {
                TokenString = null;
                return false;
            }
            else
            {
                if (givenToken == TokenString)
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