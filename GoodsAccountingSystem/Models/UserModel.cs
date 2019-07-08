
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace GoodsAccountingSystem.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required]
        public bool Activity { get; set; } = true;
    }

    public class RoleCategory
    {
        public const string Admin = "admin";
        public const string User = "user";
    }
}
