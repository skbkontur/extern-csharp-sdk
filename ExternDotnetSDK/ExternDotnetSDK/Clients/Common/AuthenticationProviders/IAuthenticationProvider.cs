namespace ExternDotnetSDK.Clients.Common.AuthenticationProviders
{
    public interface IAuthenticationProvider
    {
        string GetSessionId();
    }
}