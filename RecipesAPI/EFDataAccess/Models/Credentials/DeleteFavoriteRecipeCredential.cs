namespace RecipesAPI.Models
{
    public class DeleteFavoriteRecipeCredential
    {
        public AuthenticationCredential AuthenticationCredential { get; set; }
        public int RecipeId { get; set; }


        public DeleteFavoriteRecipeCredential(AuthenticationCredential authenticationCredential, int recipeId)
        {
            AuthenticationCredential = authenticationCredential;
            RecipeId = recipeId;
        }
    }
}