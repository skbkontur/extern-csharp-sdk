using System;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using Kontur.Extern.Api.Client.ApiLevel.Json;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Drafts.Check;
using Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Drafts.Send;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Models.Common;
using NUnit.Framework;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Json.Converters.Drafts
{
    [TestFixture]
    internal class SendFailureJsonSerialization_Tests
    {
        private const string Json = @"{
  ""id"": ""urn:error:externapi:checkProtocolHasErrors"",
  ""status-code"": 400,
  ""message"": ""Check protocol has errors"",
  ""status"": ""check-protocol-has-errors"",
  ""check-result"": {
    ""documents-errors"": {
      ""e30306d9-8508-4021-9520-ebba937a2194"": [
        {
          ""description"": ""Атрибут \u0027ИдФайл\u0027 некорректный - Значение \u0027NO_SRCHIS_0007_0007_1...ска: \u0027NO_SRCHIS_\\d(_\\d)?_(\\d|\\d)_\\d_.{1,36}\u0027\u0027."",
          ""source"": ""/Сведения о среднесписочной численности работников за предшествующий календарный год (Файл)/Идентификатор файла (@ИдФайл)"",
          ""level"": ""Error"",
          ""type"": ""Schema""
        },
        {
          ""description"": ""Атрибут \u0027НаимОрг\u0027 некорректный - Не заполнено значение."",
          ""source"": ""/Сведения о среднесписочной численности работников за предшеств...дическое лицо (НПЮЛ)/Полное наименование организации (@НаимОрг)"",
          ""level"": ""Error"",
          ""type"": ""Schema""
        },
        {
          ""description"": ""Атрибут \u0027КПП\u0027 некорректный - Значение \u0027\u0027 не проходит по следующ...ичению: \u0027Нарушен формат: Код причины постановки на учет (КПП)\u0027."",
          ""source"": ""/Сведения о среднесписочной численности работников за предшеств...ке (СвНП)/Налогоплательщик - юридическое лицо (НПЮЛ)/КПП (@КПП)"",
          ""level"": ""Error"",
          ""type"": ""Schema""
        },
        {
          ""description"": ""Не удалось определить код налогового органа в ИдФайл-е"",
          ""source"": ""ИдФайл"",
          ""level"": ""Error"",
          ""type"": ""Schema""
        }
      ]
    },
    ""common-errors"": [
      {
        ""description"": ""ИНН-КПП отправителя, указанные в идентификаторе файла (), и ИНН...ах организации-отправителя (1117147832-381550586), не совпадают"",
        ""source"": ""Отчет"",
        ""level"": ""Error"",
        ""type"": ""CrossCheckReport""
      },
      {
        ""description"": ""ФИО руководителя в отчете (Иванов,Кирилл,Адамович) не совпадает...о должны быть заполнены данные об уполномоченном представителе."",
        ""source"": ""Отчет, Сертификат"",
        ""level"": ""Error"",
        ""type"": ""CrossCheckReportCert""
      },
      {
        ""description"": ""ИНН в сертификате не совпадает с ИНН в Идентификаторе Файла"",
        ""source"": ""Отчет, Сертификат"",
        ""level"": ""Error"",
        ""type"": ""CrossCheckReport""
      }
    ]
  }
}";
        private IJsonSerializer serializer = null!;

        [SetUp]
        public void SetUp()
        {
            serializer = JsonSerializerFactory.CreateJsonSerializer();
        }
        
        [Test]
        public void Should_deserialize_send_failure_dto()
        {
            var expectedSendFailure = CreateSendFailureForJson();

            var sendFailure = serializer.Deserialize<SendFailure>(Json);
            
            sendFailure.Should().BeEquivalentTo(expectedSendFailure);
        }

        [Test]
        public void Should_serialize_send_failure_dto()
        {
            var json = serializer.SerializeToIndentedString(CreateSendFailureForJson());

            json.Should().Be(Json);
        }

        private SendFailure CreateSendFailureForJson()
        {
            var checkResult = new CheckResultData
            {
                DocumentsErrors = new Dictionary<Guid, CheckError[]>
                {
                    [Guid.Parse("e30306d9-8508-4021-9520-ebba937a2194")] = new CheckError[]
                    {
                        new()
                        {
                            Description = "Атрибут 'ИдФайл' некорректный - Значение 'NO_SRCHIS_0007_0007_1...ска: 'NO_SRCHIS_\\d(_\\d)?_(\\d|\\d)_\\d_.{1,36}''.",
                            Source = "/Сведения о среднесписочной численности работников за предшествующий календарный год (Файл)/Идентификатор файла (@ИдФайл)",
                            Level = "Error",
                            Type = "Schema"
                        },
                        new()
                        {
                            Description = "Атрибут 'НаимОрг' некорректный - Не заполнено значение.",
                            Source = "/Сведения о среднесписочной численности работников за предшеств...дическое лицо (НПЮЛ)/Полное наименование организации (@НаимОрг)",
                            Level = "Error",
                            Type = "Schema"
                        },
                        new()
                        {
                            Description = "Атрибут 'КПП' некорректный - Значение '' не проходит по следующ...ичению: 'Нарушен формат: Код причины постановки на учет (КПП)'.",
                            Source = "/Сведения о среднесписочной численности работников за предшеств...ке (СвНП)/Налогоплательщик - юридическое лицо (НПЮЛ)/КПП (@КПП)",
                            Level = "Error",
                            Type = "Schema"
                        },
                        new()
                        {
                            Description = "Не удалось определить код налогового органа в ИдФайл-е",
                            Source = "ИдФайл",
                            Level = "Error",
                            Type = "Schema"
                        }
                    }
                },
                CommonErrors = new CheckError[]
                {
                    new()
                    {
                        Description = "ИНН-КПП отправителя, указанные в идентификаторе файла (), и ИНН...ах организации-отправителя (1117147832-381550586), не совпадают",
                        Source = "Отчет",
                        Level = "Error",
                        Type = "CrossCheckReport"
                    },
                    new()
                    {
                        Description = "ФИО руководителя в отчете (Иванов,Кирилл,Адамович) не совпадает...о должны быть заполнены данные об уполномоченном представителе.",
                        Source = "Отчет, Сертификат",
                        Level = "Error",
                        Type = "CrossCheckReportCert"
                    },
                    new()
                    {
                        Description = "ИНН в сертификате не совпадает с ИНН в Идентификаторе Файла",
                        Source = "Отчет, Сертификат",
                        Level = "Error",
                        Type = "CrossCheckReport"
                    }
                }
            };
            return new SendFailure(
                Urn.Parse("urn:error:externapi:checkProtocolHasErrors"),
                HttpStatusCode.BadRequest,
                "Check protocol has errors",
                "check-protocol-has-errors",
                checkResult
            );
        }
    }
}