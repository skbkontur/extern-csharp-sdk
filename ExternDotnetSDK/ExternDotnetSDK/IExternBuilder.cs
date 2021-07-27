using JetBrains.Annotations;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public interface IExternBuilder
    {
        IExternBuilder TryResolveUnauthorizedResponsesAutomatically();
        
        IExtern Create();
    }
}