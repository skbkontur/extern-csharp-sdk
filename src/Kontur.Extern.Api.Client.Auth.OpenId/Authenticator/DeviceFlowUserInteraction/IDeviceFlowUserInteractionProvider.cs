namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.DeviceFlowUserInteraction
{
    public interface IDeviceFlowUserInteractionProvider
    {
        IDeviceFlowUserInteractionProcess InitiateUserInteraction(
            DeviceFlowUserInteractionInfo userInteractionInfo);
    }
}
