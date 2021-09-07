using System;
using Kontur.Extern.Client.Http.Configurations;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Strategies;

namespace Kontur.Extern.Client.Testing.Fakes.Http
{
    public class FakeHttpClientConfiguration : IHttpClientConfiguration
    {
        private readonly string baseUrl;
        private readonly FakeHttpMessages httpMessages;

        public FakeHttpClientConfiguration(string baseUrl, FakeHttpMessages httpMessages)
        {
            this.baseUrl = baseUrl;
            this.httpMessages = httpMessages;
        }
        
        public void Apply(IClusterClientConfiguration config)
        {
            config.SetupExternalUrl(new Uri(baseUrl));
            config.Transport = new FakeTransport(httpMessages);
            config.DefaultRequestStrategy = new SingleReplicaRequestStrategy();
        }
    }
}