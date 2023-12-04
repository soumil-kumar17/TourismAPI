using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Tourisum.Repository.Models
{
    [BsonIgnoreExtraElements]
    public class Cluster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ClusterID { get; set; }

        [BsonElement("ClusterName")]
        public string ClusterName { get; set; }

        [BsonElement("Department")]
        public string Department { get; set; }

        [BsonElement("SkillFamily")]
        public string SkillFamily { get; set; }

        [BsonElement("Technology")]
        public IEnumerable<Technology> Technology { get; set; }

        [BsonElement("CreatedBy")]
        public string CreatedBy { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreatedDate { get; set; }

        [BsonElement("UpdatedBy")]
        public string UpdatedBy { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdatedDate { get; set; }

    }
}
