using BasicOnlineTransactions.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;
using BasicOnlineTransactions.Models;

namespace BasicOnlineTransactions.Models
{
    public class RequestMessage
    {
        [Required, StringLength(50)]
        public string? partnerkey { get; set; }

        [Required, StringLength(50)]
        public string? partnerrefno { get; set; }

        [Required, StringLength(50)]
        public string? partnerpassword { get; set; }

        [Range(1, Int64.MaxValue, ErrorMessage = "Only Positive Value Allowed")]
        public long totalamount { get; set; }

        public List<ItemDetails> items { get; set; } = null;


        private DateTime _realTimeStamp;

        [JsonIgnore()]
        public DateTime RealTimeStamp
        {
            get { return _realTimeStamp; }
        }

        [Required]
        public string Timestamp
        {
            get
            {
                return _realTimeStamp.ToUniversalTime().ToString("o");
            }
            set
            {
                _realTimeStamp = DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }
        }
        public string messagesignature
        {
            get
            {
                string paramOder = DateTime.Parse(Timestamp).ToString("yyyyMMddHHmmss") + partnerkey + partnerrefno + totalamount + partnerpassword;
                return Convert.ToBase64String(SHA256Generator.ComputeHashString(paramOder)).ToString();
            }
        }
    }
}
