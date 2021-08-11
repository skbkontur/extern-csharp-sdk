using JetBrains.Annotations;
using Kontur.Extern.Client.Model.Configuration;

namespace Kontur.Extern.Client
{
    [PublicAPI]
    public interface IExternBuilder
    {
        IExternBuilder TryResolveUnauthorizedResponsesAutomatically(bool enabled = true);
        IExternBuilder OverrideContentsOptions(ContentManagementOptions options);
        
        IExtern Create();
    }
}