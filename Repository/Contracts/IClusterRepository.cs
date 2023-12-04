using Tourisum.Repository.Models;

namespace Tourisum.Repository.Contracts
{
    public interface IClusterRepository
    {
        IEnumerable<Cluster> GetAllClusters();
        bool CreateCluster(Cluster cluster);
        Task<bool> UpdateCluster(Cluster cluster);
        Task<bool> DeleteCluster(string clusterID);
    }
}
