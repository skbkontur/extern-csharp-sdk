namespace Kontur.Extern.Client.Testing.Fakes.Http
{
    public class FakeClusterClient
    {
        public FakeClusterClient(string baseUrl = "https://test/")
        {
            var httpMessages = new FakeHttpMessages();
            RetryStrategy = new FakeRetryStrategyPolicy();
            Configuration = new FakeHttpClientConfiguration(baseUrl, httpMessages, RetryStrategy);
            Setup = new FakeClusterClientSetup(httpMessages);
            Verify = new FakeClusterClientVerify(httpMessages);
        }

        public FakeClusterClientSetup Setup { get; }
        public FakeClusterClientVerify Verify { get; }
        public FakeHttpClientConfiguration Configuration { get; }
        public FakeRetryStrategyPolicy RetryStrategy { get; }
    }
}