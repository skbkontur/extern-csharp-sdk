using Kontur.Extern.Client.End2EndTests.TestEnvironment;
using Xunit;

namespace Kontur.Extern.Client.End2EndTests.Client.TestAbstractions
{
    [CollectionDefinition(Name)]
    public class IsolatedAccountEnvironmentCollection : ICollectionFixture<IsolatedAccountEnvironment>
    {
        public const string Name = nameof(IsolatedAccountEnvironmentCollection);
    }
}