using System;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.DeviceFlowUserInteraction;

public interface IDeviceFlowUserInteractionProcess : IDisposable
{
    bool IsCancelled();
}