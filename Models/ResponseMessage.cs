using System.ComponentModel.DataAnnotations;

namespace BasicOnlineTransactions.Models
{
    public class ResponseMessage
    {
        [Required]
        public string result { get; set; }

        [Required]
        public string errormessage { get; set; }
    }
}
