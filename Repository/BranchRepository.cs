using Tourism.Repository;
using Tourism.Repository.Contracts;
using Tourism.Repository.Models;
using MongoDB.Driver;

namespace Tourism.Repository
{
    public class BranchRepository : IBranchRepository
    {
        RepositoryContext _repositoryContext;
        public BranchRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        /// <summary>
        /// Create branch
        /// </summary>
        /// <param name="branch"></param>
        /// <returns></returns>
        public async Task<bool> CreateBranch(Branch branch)
        {

            try
            {
                _repositoryContext.Branches.InsertOne(branch);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Update branch
        /// </summary>
        /// <param name="branch"></param>
        /// <returns></returns>
        public async Task<bool> UpdateBranch(Branch branch)
        {
            bool flg = false;
            try
            {
                var tempBranch = Builders<Branch>.Update.Set(c => c.BranchName, branch.BranchName).Set(c => c.Place, branch.Place).Set(c => c.Website, branch.Website).Set(c => c.Contact, branch.Contact).Set(c => c.Email, branch.Email).Set(c=>c.Package,branch.Package).Set(c => c.UpdatedBy, branch.UpdatedBy).Set(c => c.UpdatedDate, branch.UpdatedDate);
                var result = await _repositoryContext.Branches.UpdateOneAsync(c => c.BranchID == branch.BranchID, tempBranch);
                if (result.ModifiedCount > 0)
                    flg = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flg;
        }

        /// <summary>
        /// Delete branch
        /// </summary>
        /// <param name="BranchID"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBranch(string BranchID)
        {
            bool flg = false;
            try
            {
                var result = await _repositoryContext.Branches.DeleteOneAsync(c => c.BranchID == BranchID);
                if (result.DeletedCount > 0)
                    flg = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flg;
        }

        //private string getNextSequence(string name)
        //{
        //    var ret = _repositoryContext.Branches.FindOneAndUpdate (
        //          {
        //            query: { _id: name },
        //            update: { $inc: { seq: 1 } },
        //            new: true
        //          }
        //        );

        //    return ret.seq;
        //}
    }
}
