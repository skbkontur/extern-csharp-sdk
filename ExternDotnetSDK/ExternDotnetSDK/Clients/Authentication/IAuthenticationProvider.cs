namespace ExternDotnetSDK.Clients.Authentication
{
    public interface IAuthenticationProvider
    {
        string GetApiKey();

        string GetSessionId();
    }
}