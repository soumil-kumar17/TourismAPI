using System.ComponentModel.DataAnnotations;
using Tourism.Repository.Models;

namespace Tourism.Repository.DTO
{
    public class UpdateBranchDto
    {
        public string? BranchID { get; set; }

        [Required(ErrorMessage = "Branch name is required")]
        public string? BranchName { get; set; }

        [Required(ErrorMessage = "Place name is required")]
        public string? Place { get; set; }

        [Required]
        public IEnumerable<Tariff>? Package { get; set; }

        [Required]
        [RegularExpression(@"^(http(s)?:\/\/www\.[a-zA-Z0-9@:%_\+~#=]{2,256}(\.[a-z]{2,6}){1,2})$", ErrorMessage = "Invalid website format")]
        //[Url(ErrorMessage = "Invalid website format")]
        public string? Website { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid mobile format")]
        public string? Contact { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Updated by required")]
        public string? UpdatedBy { get; set; }
    }
}
