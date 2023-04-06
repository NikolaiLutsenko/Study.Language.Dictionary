using System.ComponentModel.DataAnnotations;

namespace Lang.Dictionary.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotCompareAttribute : CompareAttribute
    {
        public NotCompareAttribute(string otherProperty) : base(otherProperty)
        {
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var result = base.IsValid(value, validationContext);
            if (result is null)
            {
                string[]? memberNames = validationContext.MemberName != null
                   ? new[] { validationContext.MemberName }
                   : null;

                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), memberNames);
            }
                
            return null;
        }
    }
}
