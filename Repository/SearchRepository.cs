using Tourism.Repository.Contracts;
using Tourism.Repository.Models;
using MongoDB.Driver;

namespace Tourism.Repository;

public class SearchRepository : ISearchRepository
{
    private readonly RepositoryContext _repositoryContext;
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
            return await _repositoryContext.Branches.FindAsync(x => true).Result.ToListAsync();
        }
        catch (Exception)
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
            return await _repositoryContext.Branches.FindAsync(x => x.BranchName!.ToUpper()
            .Contains(branchName.ToUpper())).Result.ToListAsync();
        }
        catch (Exception)
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
            return await _repositoryContext.Branches
                .FindAsync(x => x.BranchID == branchID).Result.ToListAsync(); 
        }
        catch (Exception)
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
            return await _repositoryContext.Branches
                .FindAsync(x => x.Place == placeName).Result.ToListAsync();
        }
        catch (Exception)
        {
            return Enumerable.Empty<Branch>();
        }
    }
}
