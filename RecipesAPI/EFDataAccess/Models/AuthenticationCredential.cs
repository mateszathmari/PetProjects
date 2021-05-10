namespace RecipesAPI.Models
{
    public class AuthenticationCredential
    {
        public string UserName { get; set; }
        public string Token { get; set; }

        public AuthenticationCredential(string userName, string token)
        {
            UserName = userName;
            Token = token;
        }
    }
}