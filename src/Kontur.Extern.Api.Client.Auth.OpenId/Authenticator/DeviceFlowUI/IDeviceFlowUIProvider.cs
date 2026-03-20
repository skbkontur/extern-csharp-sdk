using System;

namespace Kontur.Extern.Api.Client.Auth.OpenId.Authenticator.DeviceFlowUI
{
    public interface IDeviceFlowUIProvider
    {
        IDeviceFlowUI ShowUI(DeviceFlowInfo info);
    }

    public interface IDeviceFlowUI : IDisposable
    {
        bool IsCancelled();
    }

    public class DeviceFlowInfo
    {
        public string AuthorizationUri { get; set; }
        public string UriForChangeUser { get; set; }

        public TimeSpan ExpiresIn { get; set; }
    }
}
