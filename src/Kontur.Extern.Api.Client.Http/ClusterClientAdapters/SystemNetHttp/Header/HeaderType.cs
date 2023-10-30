using System;

namespace Kontur.Extern.Api.Client.Http.ClusterClientAdapters.SystemNetHttp.Header
{
    [Flags]
    internal enum HeaderType : byte
    {
        General = 1,
        Request = 2,
        Response = 4,
        Content = 8,
        Custom = 16,
        All = Custom | Content | Response | Request | General
    }
}