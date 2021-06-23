using System;

namespace Kontur.Extern.Client.Engine.EngineBuilder
{
    public interface IAuthenticatedEngine
    {
        IAuthenticatedEngine WithAccountIdProvider(IAccountIdProvider idProvider);

        IAuthenticatedEngine WithAccountId(Guid accountId);
    }
}