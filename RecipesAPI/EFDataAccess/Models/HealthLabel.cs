using System.ComponentModel.DataAnnotations;

namespace RecipesAPI.Models
{
    public class HealthLabel
    {

        [Key]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}