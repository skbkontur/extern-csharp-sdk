using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Model.Configuration;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public interface IExternBuilder
    {
        IExternBuilder TryResolveUnauthorizedResponsesAutomatically(bool enabled = true);
        IExternBuilder OverrideContentsOptions(ContentManagementOptions options);
        
        IExtern Create();
    }
}