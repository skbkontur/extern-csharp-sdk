using System;
using FluentAssertions;
using Kontur.Extern.Client.ApiLevel.Json;
using Kontur.Extern.Client.ApiLevel.Models.Common;
using Kontur.Extern.Client.ApiLevel.Models.Docflows;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Model.Docflows;
using NUnit.Framework;

namespace Kontur.Extern.Client.Tests.ApiLevel.Clients.Models.JsonConverters
{
    [TestFixture]
    internal class DocflowFilterJsonSerialization_Tests
    {
        private const string Json = @"{
  ""finished"": true,
  ""incoming"": true,
  ""skip"": 1,
  ""take"": 100,
  ""inn-kpp"": ""inn-kpp"",
  ""org-id"": ""0924103a-1ddf-4ef7-a7f3-323e0702aff4"",
  ""order-by"": ""ascending"",
  ""updated-from"": ""2021-08-27T13:51:22"",
  ""updated-to"": ""2021-08-27T13:51:22"",
  ""created-from"": ""2021-08-27T13:51:22"",
  ""created-to"": ""2021-08-27T13:51:22"",
  ""types"": [
    ""urn:docflow:fns534-letter""
  ],
  ""knd"": ""knd"",
  ""okud"": ""okud"",
  ""okpo"": ""okpo"",
  ""cu"": ""code"",
  ""reg-number"": ""reg"",
  ""form-name"": ""the-name"",
  ""demands-on-reports"": true,
  ""period-from"": ""2021-08-27T13:51:22"",
  ""period-to"": ""2021-08-27T13:51:22"",
  ""for-all-users"": true
}";
        private IJsonSerializer serializer;

        [SetUp]
        public void SetUp() => serializer = new JsonSerializerFactory()._CreateJsonSerializer();

        [Test]
        public void Should_serialize_DocflowFilter_to_json()
        {
            var json = serializer.SerializeToIndentedString(CreateDocflowFilter());

            json.Should().Be(Json);
        }

        private static DocflowFilter CreateDocflowFilter()
        {
            return new DocflowFilter
            {
                Finished = true,
                Incoming = true,
                Skip = 1,
                Take = 100,
                InnKpp = "inn-kpp",
                OrgId = Guid.Parse("0924103A-1DDF-4EF7-A7F3-323E0702AFF4"),
                OrderBy = SortOrder.Ascending,
                UpdatedFrom = new DateTime(2021, 08, 27, 13, 51, 22),
                UpdatedTo = new DateTime(2021, 08, 27, 13, 51, 22),
                CreatedFrom = new DateTime(2021, 08, 27, 13, 51, 22),
                CreatedTo = new DateTime(2021, 08, 27, 13, 51, 22),
                Types = new[]
                {
                    DocflowType.Fns.Fns534.Letter.ToUrn()
                },
                Knd = "knd",
                Okud = "okud",
                Okpo = "okpo",
                AuthorityCode = "code",
                RegNumber = "reg",
                FormName = "the-name",
                DemandsOnReports = true,
                PeriodFrom = new DateTime(2021, 08, 27, 13, 51, 22),
                PeriodTo = new DateTime(2021, 08, 27, 13, 51, 22),
                ForAllUsers = true
            };
        }
    }
}