using Tourism.Repository.Models;

namespace Tourism.Repository.Contracts;

public interface IUserProfileRepository
{

    Task<bool> CreateUser(UserProfile user);
    Task<IEnumerable<UserProfile>> GetAllUsers();
    Task<UserProfile> GetUser(string userName);
}
