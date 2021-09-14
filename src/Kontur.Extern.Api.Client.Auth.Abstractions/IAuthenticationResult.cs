using Vostok.Clusterclient.Core.Model;

namespace Kontur.Extern.Api.Client.Auth.Abstractions
{
    public interface IAuthenticationResult
    {
        Request Apply(Request request);
    }
}