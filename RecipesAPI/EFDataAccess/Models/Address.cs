using System.ComponentModel.DataAnnotations;

namespace RecipesAPI.Models
{
    public class Address
    {
        [Key] public int Id { get; set; }
        [Required] [MaxLength(100)] public string City { get; set; }
        [Required] [MaxLength(200)] public string Street { get; set; }
        [Required] [MaxLength(10)] public int HouseNumber { get; set; }
        [Required] [MaxLength(10)] public string PostCode { get; set; }

        public Address(string city, string street, int houseNumber, string postCode)
        {
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            PostCode = postCode;
        }
    }
}