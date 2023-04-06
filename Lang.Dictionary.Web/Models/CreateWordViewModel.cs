using Lang.Dictionary.Web.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Lang.Dictionary.Web.Models
{
    public class CreateWordViewModel
    {
        public Guid? Id { get; set; }

        [Display(Name = "Оберіть мову слова")]
        [Required]
        public int? FromLanguageId { get; set; }

        [Display(Name = "Введіть слово")]
        [Required(ErrorMessage = "Поле слово обов'язкове")]
        public string? FromValue { get; set; }

        [Display(Name = "Оберіть мову переклада")]
        [Required]
        [NotCompare(nameof(FromLanguageId), ErrorMessage = "Мови мають бути різними")]
        public int? ToLanguageId { get; set; }

        [Display(Name = "Введіть переклад")]
        [Required(ErrorMessage = "Поле переклад обов'язкове")]
        public string? ToValue { get; set; }
    }
}
