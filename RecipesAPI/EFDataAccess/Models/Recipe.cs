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
        public List<Ingredient> IngredientList { get; set; }
        public List<HealthLabel> HealthLabels { get; set; }
        public string RecipeLink { get; set; }
        public Recipe()
        {

        }

        public Recipe(string name, string image, int totalTime, List<Ingredient> ingeIngredientList,
            List<HealthLabel> healthLabels)
        {
            this.Name = name;
            this.Image = image;
            this.TotalTime = totalTime;
            this.IngredientList = ingeIngredientList;
            this.HealthLabels = healthLabels;
        }
    }
}