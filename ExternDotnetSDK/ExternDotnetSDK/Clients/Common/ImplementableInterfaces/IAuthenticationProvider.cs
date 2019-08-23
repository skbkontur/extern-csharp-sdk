namespace ExternDotnetSDK.Clients.Common.ImplementableInterfaces
{
    /// <summary>
    ///     Contains data for user authentication when creating requests.
    /// </summary>
    public interface IAuthenticationProvider
    {
        string GetApiKey();

        string GetSessionId();
    }
}