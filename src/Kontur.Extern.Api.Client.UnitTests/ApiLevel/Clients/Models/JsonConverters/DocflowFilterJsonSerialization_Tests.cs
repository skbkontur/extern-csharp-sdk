using System;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Json;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Docflows;
using Kontur.Extern.Api.Client.Common.Time;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.JsonConverters
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
  ""updated-from"": ""2021-08-26"",
  ""updated-to"": ""2021-08-27"",
  ""created-from"": ""2021-08-26"",
  ""created-to"": ""2021-08-27"",
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
  ""period-from"": ""2021-08-26"",
  ""period-to"": ""2021-08-27"",
  ""for-all-users"": true
}";
        private IJsonSerializer serializer;

        [SetUp]
        public void SetUp() => serializer = JsonSerializerFactory.CreateJsonSerializer();

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
                UpdatedFrom = new DateOnly(2021, 08, 26),
                UpdatedTo = new DateOnly(2021, 08, 27),
                CreatedFrom = new DateOnly(2021, 08, 26),
                CreatedTo = new DateOnly(2021, 08, 27),
                Types = new[]
                {
                    DocflowType.Fns.Fns534.Letter.ToUrn()
                },
                Knd = "knd",
                Okud = "okud",
                Okpo = "okpo",
                Cu = "code",
                RegNumber = "reg",
                FormName = "the-name",
                DemandsOnReports = true,
                PeriodFrom = new DateOnly(2021, 08, 26),
                PeriodTo = new DateOnly(2021, 08, 27),
                ForAllUsers = true
            };
        }
    }
}