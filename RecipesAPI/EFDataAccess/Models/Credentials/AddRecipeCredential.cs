namespace RecipesAPI.Models
{
    public class AddRecipeCredential
    {
        public AuthenticationCredential AuthenticationCredential { get; set; }
        public RecipeCredential RecipeCredential { get; set; }

        public AddRecipeCredential(AuthenticationCredential authenticationCredential, RecipeCredential recipeCredential)
        {
            this.AuthenticationCredential = authenticationCredential;
            this.RecipeCredential = recipeCredential;
        }
    }
}