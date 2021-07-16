using Kontur.Extern.Client.Exceptions;

namespace Kontur.Extern.Client.Authentication.OpenId.Provider.Models
{
    public class Credentials
    {
        public Credentials(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw Errors.StringShouldNotBeEmptyOrWhiteSpace(nameof(userName));
            
            if (string.IsNullOrWhiteSpace(password))
                throw Errors.StringShouldNotBeEmptyOrWhiteSpace(nameof(password));
            
            UserName = userName;
            Password = password;
        }
        
        public string UserName { get; }
        public string Password { get; }
    }
}