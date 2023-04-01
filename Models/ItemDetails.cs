using BasicOnlineTransactions.Helpers;
using System.ComponentModel.DataAnnotations;

namespace BasicOnlineTransactions.Models
{
    public class ItemDetails
    {
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Cannot be null or empty")]
        [CustomValidation(typeof(CustomValidationMethod), nameof(CustomValidationMethod.NullStringValidate))]
        public string? partneritemref { get; set; }

        [StringLength(100, MinimumLength = 1, ErrorMessage = "Cannot be null or empty")]
        [CustomValidation(typeof(CustomValidationMethod), nameof(CustomValidationMethod.NullStringValidate))]
        public string? name { get; set; }

        [Range(1, 5, ErrorMessage = "Only allow value to be > 1 and < 5")]
        public long? qty { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "Only allow positive value")]
        public long? unitprice { get; set; }
    }
}
