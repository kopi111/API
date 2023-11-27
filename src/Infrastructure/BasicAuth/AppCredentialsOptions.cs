namespace Infrastructure.BasicAuth
{
    public class AppCredentialsOptions
    {

        public string  userName { get; set; }
   
        public string password { get; set; }
       

        public bool IsUserNameConfigured => !string.IsNullOrWhiteSpace(userName);
        public bool IsPasswordConfigured => !string.IsNullOrWhiteSpace(password);
    }
}