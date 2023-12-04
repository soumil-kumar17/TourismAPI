using Tourisum.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DTO
{
    public class CreateClusterDto
    {
        public string ClusterID { get; set; }
        public string ClusterName { get; set; }
        public string Department { get; set; }
        public string SkillFamily { get; set; }
        public IEnumerable<Technology> Technology { get; set; }
        public string CreatedBy { get; set; }
    }
}
