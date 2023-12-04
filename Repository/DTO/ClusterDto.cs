using Tourisum.Repository.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace Tourisum.Repository.DTO
{
    public class ClusterDto
    {
        public string ClusterID { get; set; }
        public string ClusterName { get; set; }
        public string Department { get; set; }
        public string SkillFamily { get; set; }
        public IEnumerable<Technology> Technology { get; set; }
    }
}
