namespace RecipesAPI.Models
{
    public class AddFavoriteRecipeCredential
    {
        public int RecipeId { get; set; }
        public AuthenticationCredential AuthenticationCredential { get; set; }

        public AddFavoriteRecipeCredential(int recipeId, AuthenticationCredential authenticationCredential)
        {
            RecipeId = recipeId;
            AuthenticationCredential = authenticationCredential;
        }
    }
}