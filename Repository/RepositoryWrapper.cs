using Tourism.Repository.Contracts;
using TourismAPI.Repository;

namespace Tourism.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext? _repoContext;
        private IBranchRepository? _branch;
        private IUserProfileRepository? _user;
        private ISearchRepository? _search;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public IBranchRepository Branch
        {
            get
            {
                if (_branch == null)
                {
                    _branch = new BranchRepository(_repoContext);
                }
                return _branch;
            }
        }

        //public IUserProfileRepository User => throw new NotImplementedException();

        public IUserProfileRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserProfileRepository(_repoContext);
                }
                return _user;
            }
        }

        public ISearchRepository Search
        {
            get
            {
                if (_search == null)
                {
                    _search = new SearchRepository(_repoContext);
                }
                return _search;
            }
        }

    }
}
