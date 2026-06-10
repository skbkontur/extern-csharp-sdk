using JetBrains.Annotations;
using System;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.DeviceFlowUserInteraction;

[PublicAPI]
public interface IDeviceFlowUserInteractionProcess : IDisposable
{
    bool IsCancelled();
}