namespace BasicOnlineTransactions.Helpers
{
    public class ErrorMessageText
    {
        public const string ACCESS_DENIED = "Access Denied! Cuba lagi";
        public const string INVALID_TOTAL_AMOUNT = "Invalid Total Amount.";
        public const string EXPIRED = "Expired";
        public const string INVALID_DATE = "Invalid date.";
        public const string SUCCESS_OPERATION = "No error!";

        public string ParamRequired(string value)
        {
            return $"{value} is required.";
        }
    }
}
