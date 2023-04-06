using Lang.Dictionary.Web.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Lang.Dictionary.Web.Models.Accounts
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Пошта обов'язкова")]
        [Display(Name = "Пошта")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Введіть коректну пошту")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Ім'я обов'язкове")]
        [Display(Name = "Ім'я")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Оберіть вашу мову")]
        [Display(Name = "Ваша мова")]
        public int? BaseLanguageId { get; set; }

        [Required(ErrorMessage = "Оберіть мову яку вчите")]
        [Display(Name = "Мова яку вчите")]
        [NotCompare(nameof(BaseLanguageId), ErrorMessage = "Мова яку ви вчите не має дорівнювати ващій мові")]
        public int? StudyLanguageId { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Введіть пароль повторно")]
        [Display(Name = "Повторіть пароль")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string RepeatePassword { get; set; } = null!;
    }
}
