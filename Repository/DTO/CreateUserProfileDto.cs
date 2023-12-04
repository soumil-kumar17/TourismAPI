﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourism.Repository.DTO
{
    public class CreateUserProfileDto
    {
        //public string UserID { get; set; }

        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password name is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Role name is required")]
        [RegularExpression("User|Admin", ErrorMessage = "The role name must be either 'User' or 'Admin' only.")]
        public string? Role { get; set; }

        [Required(ErrorMessage = "CreatedBy name is required")]
        public string? CreatedBy { get; set; }

    }
}
