using Tourism.Repository.Models;

namespace Tourism.Repository.Contracts
{
    public interface IBranchRepository
    {
        Task<bool> CreateBranch(Branch branch);
        Task<bool> UpdateBranch(Branch branch);
        Task<bool> DeleteBranch(string branchID);
    }
}
