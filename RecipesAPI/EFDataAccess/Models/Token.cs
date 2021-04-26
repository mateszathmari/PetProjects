using System.ComponentModel.DataAnnotations;

namespace RecipesAPI.Models
{
    public class Token
    {
        [Key]
        public int Id { get; set; }
        [Required][MaxLength(50)]
        public string TokenString { get; set; }
    }
}