using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions;
using Kontur.Extern.Api.Client.Models.Docflows.Documents;
using Kontur.Extern.Api.Client.Models.Docflows.Enums;
using Kontur.Extern.Api.Client.Http.Serialization.SysTextJson.Extensions;

namespace Kontur.Extern.Api.Client.ApiLevel.Json.Converters.Docflows
{
    internal class DocflowConverter : JsonConverter<IDocflow>
    {
        private readonly Utf8String typePropName = "type";

        public override bool CanConvert(Type typeToConvert) =>
            typeof (IDocflow).IsAssignableFrom(typeToConvert);

        public override void Write(Utf8JsonWriter writer, IDocflow? value, JsonSerializerOptions options)
        {
            if (value is not null)
            {
                JsonSerializer.Serialize(writer, new SerializationDocflow<object>(value), options);
            }
            else
            {
                writer.WriteNullValue();
            }
        }

        public override IDocflow? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var descriptionType = GetDescriptionType(reader, typePropName);
            // todo: optimize it by caching
            var docflowType = typeof (SerializationDocflow<>).MakeGenericType(descriptionType);
            var serializationDocflow = (ISerializationDocflow?) JsonSerializer.Deserialize(ref reader, docflowType, options);
            return serializationDocflow?.ConvertToDocflow();

            static Type GetDescriptionType(Utf8JsonReader readerClone, in Utf8String typePropName)
            {
                if (!readerClone.ScanToPropertyValue(typePropName.AsUtf8()))
                    return typeof (UnknownDescription);

                var docflowTypeValue = readerClone.GetString();
                if (docflowTypeValue == null)
                    return typeof (UnknownDescription);

                var docflowType = new DocflowType(docflowTypeValue);
                return DocflowDescriptionTypes.TryGetDescriptionType(docflowType) ??
                       typeof (UnknownDescription);
            }
        }

        private interface ISerializationDocflow
        {
            IDocflow ConvertToDocflow();
        }

        private class SerializationDocflow<TDescription> : ISerializationDocflow
        {
            [JsonConstructor]
            public SerializationDocflow()
            {
            }

            public SerializationDocflow(IDocflowWithDocuments docflow)
            {
                Type = docflow.Type;
                Status = docflow.Status;
                SuccessState = docflow.SuccessState;
                Links = docflow.Links;
                Id = docflow.Id;
                OrganizationId = docflow.OrganizationId;
                SendDateTime = docflow.SendDateTime;
                LastChangeDateTime = docflow.LastChangeDateTime;
                Description = (TDescription) (object) docflow.Description;
                Documents = docflow.Documents;
            }

            public SerializationDocflow(IDocflow docflow)
            {
                Type = docflow.Type;
                Status = docflow.Status;
                SuccessState = docflow.SuccessState;
                Links = docflow.Links;
                Id = docflow.Id;
                OrganizationId = docflow.OrganizationId;
                SendDateTime = docflow.SendDateTime;
                LastChangeDateTime = docflow.LastChangeDateTime;
                Description = (TDescription) (object) docflow.Description;
                Documents = docflow is IDocflowWithDocuments docflowWithDocuments 
                    ? docflowWithDocuments.Documents 
                    : null!;
            }

            [UsedImplicitly]
            public Guid Id { get; set; }
            [UsedImplicitly]
            public Guid OrganizationId { get; set; }
            [UsedImplicitly]
            public DocflowType Type { get; set; }
            [UsedImplicitly]
            public DocflowStatus Status { get; set; }
            [UsedImplicitly]
            public DocflowState SuccessState { get; set; }
            [UsedImplicitly]
            public List<Document>? Documents { get; set; }
            [UsedImplicitly]
            public List<Link> Links { get; set; } = null!;
            [UsedImplicitly]
            [JsonPropertyName("send-date")]
            public DateTime SendDateTime { get; set; }
            [UsedImplicitly]
            [JsonPropertyName("last-change-date")]
            public DateTime? LastChangeDateTime { get; set; }
            [UsedImplicitly]
            public TDescription Description { get; set; } = default!;

            public IDocflow ConvertToDocflow()
            {
                var description = (DocflowDescription)(object)Description!;
                return Documents is not null
                    ? new Docflow(Id, OrganizationId, Type, Status, SuccessState, Documents, Links, SendDateTime, LastChangeDateTime, description)
                    : new Docflow(Id, OrganizationId, Type, Status, SuccessState, Links, SendDateTime, LastChangeDateTime, description);
            }
        }
    }
}