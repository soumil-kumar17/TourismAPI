using Tourism.Repository;
using Tourism.Repository.Contracts;
using Tourism.Repository.Models;
using MongoDB.Driver;

namespace TourismAPI.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        RepositoryContext _repositoryContext;
        public UserProfileRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        /// <summary>
        /// Create userprofile
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> CreateUser(UserProfile user)
        {

            try
            {
                _repositoryContext.Users.InsertOne(user);
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
                return _repositoryContext.Users.Find(x => true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
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
                return _repositoryContext.Users.Find(x => x.UserName.ToUpper() == userName.ToUpper()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
