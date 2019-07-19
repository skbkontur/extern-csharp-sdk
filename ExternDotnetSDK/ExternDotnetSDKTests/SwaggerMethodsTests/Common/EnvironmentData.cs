using System.Collections.Generic;
using ExternDotnetSDK.Accounts;
using ExternDotnetSDK.Common;
using ExternDotnetSDK.Organizations;

#pragma warning disable 649

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Common
{
    internal class EnvironmentData
    {
        public string Login;
        public string Password;
        public string ApiKey;
        public string AuthAddress;
        public string BaseAddress;
        public IEnumerable<Link> RootIndexLinks;
        public AccountList FullAccountList;
        public Organization Organization;
    }
}