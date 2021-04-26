using System.Collections.Generic;

namespace RecipesAPI.Models
{
    public interface IUserRepository
    {
        User GetUser(int id);
        IEnumerable<User> GetUsers();
        User AddUser(User person);
        User updateUser(User personChanges);
        User DeleteUser(int id);
    }
}