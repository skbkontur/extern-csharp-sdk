using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ExternDotnetSDKTests.SwaggerMethodsTests.APIs;
using ExternDotnetSDKTests.SwaggerMethodsTests.Common;
using Newtonsoft.Json;
using NUnit.Framework;
using Refit;

namespace ExternDotnetSDKTests.SwaggerMethodsTests.Tests
{
    internal class AllTestsShould<TApi>
    {
        private const string DataPath = "Environment.txt";

        private IAuthApi authApi;
        private SessionResponse session;
        private HttpClient client;
        
        protected EnvironmentData Data;
        protected TApi Api;

        [OneTimeSetUp]
        public virtual async Task SetUp()
        {
            using (var file = File.OpenText(DataPath))
                using (var reader = new JsonTextReader(file))
                    Data = new JsonSerializer().Deserialize<EnvironmentData>(reader);
            authApi = RestService.For<IAuthApi>(Data.AuthAddress);
            session = await authApi.ByPass(Data.Login, Data.Password, Data.ApiKey);
            client = new HttpClient(new MyHttpClientHandler(Data.ApiKey, session.Sid, Data.BaseAddress))
            {
                BaseAddress = new Uri(Data.BaseAddress)
            };
            Api = RestService.For<TApi>(client);
        }
    }
}