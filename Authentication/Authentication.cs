namespace BasicOnlineTransactions.Authentication
{
    public class Authentication: IAuthentication
    {

        private readonly IConfiguration _configuration;
        public Authentication(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool ValidateUser(string username, string password)
        {
            List<string> partnerkey = _configuration.GetSection("users").Get<string[]>().ToList();
            List<string> partnerpassword = _configuration.GetSection("passwords").Get<string[]>().ToList();

            // Get the users and passwords from user secret/appsetting.json

            if (partnerkey.Any(x => x == username) && partnerpassword.Any(x => x == password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
