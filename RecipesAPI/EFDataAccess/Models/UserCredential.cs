namespace RecipesAPI.Models
{
    public class UserCredential
    {
        public string Username { get; set; }
        public string  Password { get; set; }

        public UserCredential(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}