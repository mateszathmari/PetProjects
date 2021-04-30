namespace RecipesAPI.Models
{
    public class AuthenticationCred
    {
        public string UserName { get; set; }
        public string Token { get; set; }

        public AuthenticationCred(string userName, string token)
        {
            UserName = userName;
            Token = token;
        }
    }
}