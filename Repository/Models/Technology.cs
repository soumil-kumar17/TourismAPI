using Repository.Models;

namespace Tourisum.Repository.Models
{
    public class Technology
    {
        public string TechName { get; set; }
        public IEnumerable<Topic>? Topics { get; set; }
    }
}
