using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoodsAccountingSystem.Models
{
    public class UserModel : IdentityUser<int>
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
        public string Password { get; set; }
        [Required]
        public bool Activity { get; set; } = true;

        public UserModel(string userName) : base(userName)
        {            

        }
    }
}
