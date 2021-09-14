using Kontur.Extern.Api.Client.End2EndTests.TestEnvironment;
using Xunit;

namespace Kontur.Extern.Api.Client.End2EndTests.Client.TestAbstractions
{
    [CollectionDefinition(Name)]
    public class IsolatedAccountEnvironmentCollection : ICollectionFixture<IsolatedAccountEnvironment>
    {
        public const string Name = nameof(IsolatedAccountEnvironmentCollection);
    }
}