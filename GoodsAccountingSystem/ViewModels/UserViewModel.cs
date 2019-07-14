using System;
using System.ComponentModel.DataAnnotations;

namespace GoodsAccountingSystem.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string Password { get; set; }      

        [Required]
        [Display(Name ="Имя")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name ="Фамилия")]
        public string SecondName { get; set; }

        [Required]
        [Display(Name ="Отчество")]
        public string MiddleName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Дата рождения")]
        public DateTime RegisterDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Дата регистрации")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name ="Заблокирован")]
        public bool Activity { get; set; } = true;

        [Required]
        [Display(Name ="Роль")]
        public string UserRole { get; set; }
    }

    public class DeleteUserViewModel
    {
        [Required]
        public string Id { get; set; }       
        
        public string FirstName { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string SecondName { get; set; }

        public string MiddleName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата рождения")]
        public DateTime RegisterDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата регистрации")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Заблокирован")]
        public bool Activity { get; set; } = true;

        [Display(Name = "Роль")]
        public string UserRole { get; set; }
    }

    public class UserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }
    }

    public class CreateUserViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public string UserRole { get; set; }
    }

    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
