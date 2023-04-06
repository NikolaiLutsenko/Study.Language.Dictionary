using System.ComponentModel.DataAnnotations;

namespace Lang.Dictionary.Web.Models.Accounts
{
    public class LoginViewModel
    {
        [Display(Name = "Введіть пошту")]
        [Required(ErrorMessage = "Поле обов'язкове")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Display(Name = "Введіть пароль")]
        [Required(ErrorMessage = "Поле обов'язкове")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
