namespace Kontur.Extern.Client
{
    internal interface IExternCredentials
    {
        bool IsAdmin { get; } // NOTE: it enable loading certs and warrants for all users
    }
}