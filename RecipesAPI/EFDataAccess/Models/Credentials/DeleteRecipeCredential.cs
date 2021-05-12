namespace RecipesAPI.Models
{
    public class DeleteRecipeCredential
    {
        public AuthenticationCredential AuthenticationCredential { get; set; }
        public int RecipeId { get; set; }

        public DeleteRecipeCredential(AuthenticationCredential authenticationCredential, int recipeId)
        {
            AuthenticationCredential = authenticationCredential;
            RecipeId = recipeId;
        }
    }
}