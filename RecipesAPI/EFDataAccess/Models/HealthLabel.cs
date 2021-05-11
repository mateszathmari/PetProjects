using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipesAPI.Models
{
    public class HealthLabel
    {
        [Key] public int Id { get; set; }
        [MaxLength(100)] public string Name { get; set; }

        // public HashSet<Recipe> Recipes { get; set; } = new HashSet<Recipe>();
        public ICollection<RecipeHealthLabels> RecipeHealthLabels { get; set; } = new HashSet<RecipeHealthLabels>();

        public HealthLabel()
        {
        }

        public HealthLabel(string name)
        {
            Name = name;
        }
    }
}