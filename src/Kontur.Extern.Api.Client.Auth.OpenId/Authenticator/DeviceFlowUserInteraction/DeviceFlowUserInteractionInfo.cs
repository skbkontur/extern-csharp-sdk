using JetBrains.Annotations;
using System;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.DeviceFlowUserInteraction;

[PublicAPI]
public class DeviceFlowUserInteractionInfo
{
    public string VerificationUriComplete { get; set; }

    public string ForcedUserReloginVerificationUri  { get; set; }

    public TimeSpan ExpiresIn { get; set; }
}