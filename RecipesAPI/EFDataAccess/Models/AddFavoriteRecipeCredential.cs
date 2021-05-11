namespace RecipesAPI.Models
{
    public class AddFavoriteRecipeCredential
    {
        public string Username { get; set; }
        public Recipe Recipe { get; set; }
        public AuthenticationCredential AuthenticationCredential { get; set; }

        public AddFavoriteRecipeCredential(string username, Recipe recipe, AuthenticationCredential authenticationCredential)
        {
            Username = username;
            Recipe = recipe;
            AuthenticationCredential = authenticationCredential;
        }
    }

    
}