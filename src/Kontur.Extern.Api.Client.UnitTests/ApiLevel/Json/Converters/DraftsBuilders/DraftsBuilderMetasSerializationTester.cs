using System;
using Kontur.Extern.Api.Client.ApiLevel.Json;
using Kontur.Extern.Api.Client.Http.Serialization;
using Kontur.Extern.Api.Client.Models.DraftsBuilders;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.Enums;
using Kontur.Extern.Api.Client.UnitTests.ApiLevel.Clients.Models.TestDtoGenerators.DraftsBuilders;

namespace Kontur.Extern.Api.Client.UnitTests.ApiLevel.Json.Converters.DraftsBuilders
{
    internal class DraftsBuilderMetasSerializationTester<TMeta, TData>
        where TMeta : class, IDraftsBuilderMeta<TData>
        where TData : class
    {
        private readonly IJsonSerializer serializer;
        private readonly DraftsBuilderMetasGenerator<TMeta, TData> metaGenerator;
        private readonly DraftBuilderType unknownBuilderType = new(DraftBuilderType.Namespace.Append("unknown-builder"));

        public DraftsBuilderMetasSerializationTester(Func<TData> unknownDataFactory)
        {
            serializer = JsonSerializerFactory.CreateJsonSerializer(ignoreNullValues: false);
            metaGenerator = new DraftsBuilderMetasGenerator<TMeta, TData>(unknownDataFactory);
        }

        public (string json, TMeta meta) GenerateWithData<T>(DraftBuilderType builderType)
            where T:TData
        {
            return GenerateWithData(typeof (T), builderType);
        }

        public (string json, TMeta meta) GenerateWithData(Type? builderDataType, DraftBuilderType builderType)
        {
            var builderMeta = metaGenerator.GenerateWithData(builderDataType, builderType);
            return (Serialize(builderMeta), builderMeta);
        }
        
        public (string json, TMeta meta) GenerateWithUnknownTypeAndDataOf<T>()
            where T:TData
        {
            return GenerateWithData(typeof (T), unknownBuilderType);
        }
        
        public (string json, TMeta meta) GenerateWithoutBuilderType(TData? data)
        {
            var dummyKnownBuilderType = DraftBuilderType.Fns.BusinessRegistration.Registration;
            var meta = metaGenerator.GenerateWithoutData(dummyKnownBuilderType);
            meta.BuilderType = default;
            meta.BuilderData = data!;
            return (Serialize(meta), meta);
        }

        public TMeta Deserialize(string json) => serializer.Deserialize<TMeta>(json);

        private string Serialize(TMeta? meta)
        {
            var json = serializer.SerializeToIndentedString(meta);
            Console.WriteLine($"Generated JSON: {json}");
            return json;
        }
    }
}