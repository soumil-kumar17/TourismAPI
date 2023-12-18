using System.ComponentModel.DataAnnotations;

namespace Tourism.Repository.DTO; 
public class TariffDto
{
    [Required(ErrorMessage = "Place name required")]
    [RegularExpression("ANDAMAN|THAILAND|DUBAI|SINGAPORE|MALAYSIA", ErrorMessage = "The place name must be from ANDAMAN,THAILAND,DUBAI,SINGAPORE,MALAYSIA only.")]
    public string? Place { get; set; }

    [Required]
    [Range(50000.00, 100000.00, ErrorMessage = "Can only be between 50000 - 100000")]
    [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
    public double Price { get; set; }
}
