namespace RecipesAPI.Models
{
    public class RegistrationCred
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string PostCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }



        public RegistrationCred(string userName, string password, string email, string city, string street,
            int houseNumber, string postCode)
        {
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            PostCode = postCode;
            UserName = userName;
            Password = password;
            Email = email;
        }
    }
}