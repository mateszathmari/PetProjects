using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipesAPI.Models
{
    public class Recipe
    {
        [Key] public int id { get; set; }
        [Required] public string Name { get; set; }

        public string Image { get; set; }
        public int TotalTime { get; set; }

        public ICollection<UserRecipe> UserRecipes { get; set; } = new HashSet<UserRecipe>();
        public ICollection<RecipeHealthLabels> RecipeHealthLabels { get; set; } = new HashSet<RecipeHealthLabels>();
        public ICollection<RecipeIngredients> RecipeIngredients { get; set; } = new HashSet<RecipeIngredients>();
        public string RecipeLink { get; set; }

        public Recipe()
        {
        }

        public Recipe(string name, string image, int totalTime, string recipeLink)
        {
            this.Name = name;
            this.Image = image;
            this.TotalTime = totalTime;
            this.RecipeLink = recipeLink;
        }
    }
}