namespace Kontur.Extern.Client
{
    internal interface IExternFactory
    {
        IExtern Create(IExternCredentials credentials);
    }
}