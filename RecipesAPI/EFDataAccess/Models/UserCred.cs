namespace RecipesAPI.Models
{
    public class UserCred
    {
        public string Username { get; set; }
        public string  Password { get; set; }

        public UserCred(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}