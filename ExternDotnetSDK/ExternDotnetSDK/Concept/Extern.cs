namespace Kontur.Extern.Client
{
    interface IExternContextFactory
    {
        IExternContext Create(IExternCredentials credentials);
    }
    
    internal interface IExternCredentials
    {
        bool IsAdmin { get; } // NOTE: it enable loading certs and warrants for all users
    }

    interface IExternContext
    {
        IAccountListContext Accounts { get; }
    }
}