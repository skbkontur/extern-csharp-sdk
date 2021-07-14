using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Client.Authentication
{
    public interface IAuthenticationResult
    {
        Request Apply(Request request);
    }
}