using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Tourism.Repository.Models;

[BsonIgnoreExtraElements]
public class Branch
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? BranchID { get; set; }

    [BsonElement("BranchName")]
    [Required(ErrorMessage = "Branch name is required")]
    [MemberNotNull]
    public string? BranchName { get; set; }

    [BsonElement("Place")]
    [Required(ErrorMessage = "Place name is required")]
    public string? Place { get; set; }
    
    [BsonElement("Package")]
    public IEnumerable<Tariff>? Package { get; set; }

    [BsonElement("Website")]
    [Required]
    [RegularExpression(@"^(http(s)?:\/\/www\.[a-zA-Z0-9@:%_\+~#=]{2,256}(\.[a-z]{2,6}){1,2})$", ErrorMessage = "Invalid website format")]
    //[Url (ErrorMessage = "Invalid website format")]
    public string? Website { get; set; }

    [BsonElement("Contact")]
    [Required]
    [StringLength(10, MinimumLength = 10)]
    [RegularExpression(@"^(\d{10})$", ErrorMessage = "Invalid mobile format")]
    public string? Contact { get; set; }

    [BsonElement("Email")]
    [Required(ErrorMessage = "Invalid email format")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string? Email { get; set; }

    [BsonElement("CreatedBy")]
    public string? CreatedBy { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime CreatedDate { get; set; }

    [BsonElement("UpdatedBy")]
    public string? UpdatedBy { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    public DateTime UpdatedDate { get; set; }
}
