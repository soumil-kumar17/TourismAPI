using Tourisum.Repository.Models;

namespace Repository.DTO
{
    public class UpdateClusterDto
    {
        public string ClusterID { get; set; }
        public string ClusterName { get; set; }
        public string Department { get; set; }
        public string SkillFamily { get; set; }
        public IEnumerable<Technology> Technology { get; set; }
        public string UpdatedBy { get; set; }
    }
}
