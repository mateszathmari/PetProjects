

using System.ComponentModel.DataAnnotations;

namespace RecipesAPI.Models
{
    public class Recipe
    {
        [Key] public int id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}