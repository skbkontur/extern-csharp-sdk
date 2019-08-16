namespace ExternDotnetSDK.Clients.Authentication
{
    public interface IAuthenticationProvider
    {
        IAuthClientRefit ClientRefit { get; }

        string GetApiKey();

        string GetSessionId();
    }
}