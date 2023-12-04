using Tourism.Repository.Models;

namespace Tourism.Repository.Contracts
{
    public interface ISearchRepository
    {
        Task<IEnumerable<Branch>> GetAllBranches();
        Task<IEnumerable<Branch>> GetBranchByBranchId(string branchID);
        Task<IEnumerable<Branch>> GetBranchesByBranchName(string branchName);
        Task<IEnumerable<Branch>> GetBranchesByPlace(string placeName);
    }
}
