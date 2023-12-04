using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism.Repository.DTO
{
    public class UserDto
    {
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }
    }
}
