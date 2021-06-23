using Kontur.Extern.Client.Engine.Services.Docflows;

namespace Kontur.Extern.Client.Engine
{
    public interface IEngine
    {
        IDocflowsService DocflowsService { get; }

        IKeApiClient KeApiClient { get; }
    }
}