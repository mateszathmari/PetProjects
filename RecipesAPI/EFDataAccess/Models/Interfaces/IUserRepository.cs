using System.Collections.Generic;

namespace RecipesAPI.Models
{
    public interface IUserRepository
    {
        User GetUser(string username);
        IEnumerable<User> GetUsers();
        User AddUser(User person);
        User UpdateUser(User personChanges);
        User DeleteUser(string username);
        string GenerateTokenForUser(string userName);
        string DeleteUserToken(string userName);
    }
}