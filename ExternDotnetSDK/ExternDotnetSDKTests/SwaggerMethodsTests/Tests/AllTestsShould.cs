using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDKTests.SwaggerMethodsTests.Common;
using Newtonsoft.Json;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    [TestFixture]
    internal class AllTestsShould
    {
        private const string DataPath = "Environment.txt";

        private IAuthApi authApi;
        private SessionResponse session;

        protected HttpClient Client;
        protected EnvironmentData Data;

        [OneTimeSetUp]
        public virtual async Task SetUp()
        {
            using (var file = File.OpenText(DataPath))
                using (var reader = new JsonTextReader(file))
                    Data = new JsonSerializer().Deserialize<EnvironmentData>(reader);
            authApi = RestService.For<IAuthApi>(Data.AuthAddress);
            session = await authApi.ByPass(Data.Login, Data.Password, Data.ApiKey);
            Client = new HttpClient(new MyHttpClientHandler(Data.ApiKey, session.Sid, Data.BaseAddress))
            {
                BaseAddress = new Uri(Data.BaseAddress)
            };
        }
    }
}