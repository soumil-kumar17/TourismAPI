using Tourism.Repository;
using Tourism.Repository.Contracts;
using Tourism.Repository.Models;
using MongoDB.Driver;

namespace TourismAPI.Repository;

public class UserProfileRepository : IUserProfileRepository
{
    RepositoryContext _repositoryContext;
    public UserProfileRepository(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    /// <summary>
    /// Create user profile
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<bool> CreateUser(UserProfile user)
    {

        try
        {
            await _repositoryContext.Users.InsertOneAsync(user);
            return true;

        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<UserProfile>> GetAllUsers()
    {
        try
        {
            return await _repositoryContext.Users.FindAsync(x => true).Result.ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Get user by username
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public async Task<UserProfile> GetUser(string userName)
    {
        try
        {
            return await _repositoryContext.Users.FindAsync(x => x.UserName.ToUpper() == userName.ToUpper()).Result.FirstOrDefaultAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
