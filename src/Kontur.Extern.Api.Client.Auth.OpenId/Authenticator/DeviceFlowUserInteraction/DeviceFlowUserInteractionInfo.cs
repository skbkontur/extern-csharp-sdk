using System;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.DeviceFlowUserInteraction;

public class DeviceFlowUserInteractionInfo
{
    public string VerificationUriComplete { get; set; }

    public string VerificationUriForForcedUserReLogin  { get; set; }

    public TimeSpan ExpiresIn { get; set; }
}