using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Paths;

namespace Kontur.Extern.Api.Client
{
    [PublicAPI]
    public interface IExtern
    {
        public AccountListPath Accounts { get; }
        public EventsListPath Events { get; }

        [ClientDocumentationSection]
        public IExternClientServices Services { get; }

        public Task ReauthenticateAsync(TimeSpan? timeout = null);
    }
}