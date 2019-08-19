namespace ExternDotnetSDK.Clients.Common
{
    public interface IAuthenticationProvider
    {
        string GetApiKey();

        string GetSessionId();
    }
}