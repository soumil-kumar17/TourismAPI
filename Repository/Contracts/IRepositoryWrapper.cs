namespace Tourism.Repository.Contracts
{
    public interface IRepositoryWrapper
    {
        IBranchRepository Branch { get; }
        ISearchRepository Search { get; }
        IUserProfileRepository User { get; }
    }
}