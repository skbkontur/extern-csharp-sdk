using System.Collections.Generic;
using ExternDotnetSDK.Accounts;
using ExternDotnetSDK.Common;

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

        //public string TestAccountInn = "1754462785";
        //public string TestAccountKpp = "515744582";
        //public string TestExistingAccountId = "705f5b6e-d52f-4f6a-9ad0-66554930496b";
        //public string TestOrganizationInn = "8974038687";
        //public string TestOrganizationKpp = "687001709";
    }
}