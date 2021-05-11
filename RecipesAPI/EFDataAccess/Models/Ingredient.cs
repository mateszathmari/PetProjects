using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipesAPI.Models
{
    public class Ingredient
    {
        [Key] public int Id { get; set; }

        [MaxLength(100)] public string Name { get; set; }

        //public List<Recipe> Recipes { get; set; } = new List<Recipe>();
        public ICollection<RecipeIngredients> RecipeIngredients { get; set; } = new HashSet<RecipeIngredients>();

        public Ingredient()
        {
        }

        public Ingredient(string name)
        {
            Name = name;
        }
    }
}