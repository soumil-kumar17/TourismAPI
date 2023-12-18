using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Tourism.Repository.Models;

public class UserProfile
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserID { get; set; }

    [BsonElement("UserName")]
    [Required(ErrorMessage = "User name is required")]
    public string? UserName { get; set; }

    [BsonElement("Password")]
    [Required(ErrorMessage = "Password name is required")]
    public string? Password { get; set; }

    [BsonElement("Role")]
    [Required(ErrorMessage = "Role name is required")]
    [RegularExpression("User|Admin", ErrorMessage = "The role name must be either 'User' or 'Admin' only.")]
    public string? Role { get; set; }

    [BsonElement("CreatedBy")]
    public string? CreatedBy { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime CreatedDate { get; set; }

    [BsonElement("UpdatedBy")]
    public string? UpdatedBy { get; set; }

    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    public DateTime UpdatedDate { get; set; }
}
