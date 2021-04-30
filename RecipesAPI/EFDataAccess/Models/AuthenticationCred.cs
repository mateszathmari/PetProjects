namespace RecipesAPI.Models
{
    public class AuthenticationCred
    {
        public string UserName;
        public string Token;

        public AuthenticationCred(string userName, string token)
        {
            UserName = userName;
            Token = token;
        }
    }
}