using System.ComponentModel.DataAnnotations;

namespace BasicOnlineTransactions.Helpers
{
    public class CustomValidationMethod
    {
        public static ValidationResult NullStringValidate(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return new ValidationResult("value must not null");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
