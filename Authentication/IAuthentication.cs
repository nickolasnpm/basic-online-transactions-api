namespace BasicOnlineTransactions.Authentication
{
    public interface IAuthentication
    {
        bool ValidateUser(string username, string password);
    }
}
