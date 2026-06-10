using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.DeviceFlowUserInteraction
{
    [PublicAPI]
    public interface IDeviceFlowUserInteractionProvider
    {
        IDeviceFlowUserInteractionProcess InitiateUserInteraction(
            DeviceFlowUserInteractionInfo userInteractionInfo);
    }
}
