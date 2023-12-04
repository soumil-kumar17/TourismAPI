using Tourism.Repository.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace Tourism.Repository.DTO
{
    public class BranchDto
    {
        public string? BranchID { get; set; }
        public string? BranchName { get; set; }
        public string? Place { get; set; }
        public IEnumerable<Tariff>? Package { get; set; }
        public string? Website { get; set; }
        public string? Contact { get; set; }
        public string? Email { get; set; }

    }
}
