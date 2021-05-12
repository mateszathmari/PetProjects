using System.ComponentModel.DataAnnotations;

namespace RecipesAPI.Models
{
    public class RecipeHealthLabels
    {
        [Key] public int Id { get; set; }
        public int HealthLabelId { get; set; }
        public HealthLabel HealthLabel { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public RecipeHealthLabels()
        {
        }

        public RecipeHealthLabels(HealthLabel healthLabel, Recipe recipe)
        {
            HealthLabelId = healthLabel.Id;
            HealthLabel = healthLabel;
            RecipeId = recipe.id;
            Recipe = recipe;
        }
    }
}