using Tourism.Repository;
using Tourism.Repository.Contracts;
using Tourism.Repository.Models;
using MongoDB.Driver;

namespace Tourism.Repository
{
    public class SearchRepository : ISearchRepository
    {
        RepositoryContext _repositoryContext;
        public SearchRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        /// <summary>
        /// Get all branches
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Branch>> GetAllBranches()
        {
            try
            {
                return _repositoryContext.Branches.Find(x => true).ToList();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Branch>();
            }
        }

        /// <summary>
        /// Get branches by BranchName
        /// </summary>
        /// <param name="branchName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Branch>> GetBranchesByBranchName(string branchName)
        {
            try
            {
                return _repositoryContext.Branches.Find(x=> x.BranchName.ToUpper().Contains(branchName.ToUpper())).ToList();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Branch>();
            }
        }

        /// <summary>
        /// Get branch by BranchID
        /// </summary>
        /// <param name="branchID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Branch>> GetBranchByBranchId(string branchID)
        {
            try
            {
                return _repositoryContext.Branches.Find(x => x.BranchID == branchID).ToList(); 
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Branch>();
            }
        }

        /// <summary>
        /// Get branches by PlaceName
        /// </summary>
        /// <param name="placeName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Branch>> GetBranchesByPlace(string placeName)
        {
            try
            {
                return _repositoryContext.Branches.Find(x => x.Place == placeName).ToList();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Branch>();
            }
        }

    }
}
