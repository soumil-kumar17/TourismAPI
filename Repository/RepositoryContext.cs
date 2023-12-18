using Tourism.Repository.Models;
using MongoDB.Driver;

namespace Tourism.Repository;

public class RepositoryContext
{
    public RepositoryContext(TourismDBSettings settings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        Branches = database.GetCollection<Branch>(settings.BranchCollectionName);
        Users = database.GetCollection<UserProfile>(settings.UserProfileCollectionName);
    }

    public IMongoCollection<Branch> Branches { get; }
    public IMongoCollection<UserProfile> Users { get; }
}
