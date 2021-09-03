#nullable enable
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Kontur.Extern.Client.ApiLevel.Models.Docflows.Documents;
using Kontur.Extern.Client.ApiLevel.Models.Docflows.Documents.Requisites;
using Kontur.Extern.Client.Exceptions;
using Kontur.Extern.Client.Http.Serialization.SysTextJson.Extensions;
using Kontur.Extern.Client.Model.Documents;

namespace Kontur.Extern.Client.ApiLevel.Json.Converters.Docflows
{
    internal class DocflowDocumentDescriptionConverter : JsonConverter<DocflowDocumentDescription>
    {
        private readonly Utf8String typePropName = "type";
        
        public override DocflowDocumentDescription? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var requisitesType = GetRequisitesType(reader, typePropName);
            // todo: optimize it by caching
            var descriptionType = typeof (SerializationDocumentDescription<>).MakeGenericType(requisitesType);
            var deserialized = (ISerializationDocumentDescription?) JsonSerializer.Deserialize(ref reader, descriptionType, options);
            return deserialized?.ConvertToDto();

            static Type GetRequisitesType(Utf8JsonReader readerClone, in Utf8String typePropName)
            {
                if (!readerClone.ScanToPropertyValue(typePropName.AsUtf8()))
                    throw Errors.JsonDoesNotContainProperty(typePropName.ToString());

                var documentTypeValue = readerClone.GetString();
                if (documentTypeValue == null)
                    return typeof (CommonDocflowDocumentRequisites);

                var documentType = new DocumentType(documentTypeValue);
                return DocumentDescriptionRequisitesTypes.GetRequisiteType(documentType);
            }
        }

        public override void Write(Utf8JsonWriter writer, DocflowDocumentDescription value, JsonSerializerOptions options) => 
            JsonSerializer.Serialize(writer, new SerializationDocumentDescription<object>(value), options);

        private interface ISerializationDocumentDescription
        {
            DocflowDocumentDescription ConvertToDto();
        }

        private class SerializationDocumentDescription<TRequisites> : DocflowDocumentDescriptionBase<TRequisites>, ISerializationDocumentDescription
            where TRequisites : class
        {
            [JsonConstructor]
            public SerializationDocumentDescription()
            {
            }

            public SerializationDocumentDescription(DocflowDocumentDescription description)
            {
                Type = description.Type;
                Filename = description.Filename;
                ContentType = description.ContentType;
                DecryptedContentSize = description.DecryptedContentSize;
                EncryptedContentSize = description.EncryptedContentSize;
                Compressed = description.Compressed;
                Requisites = (TRequisites) (object) description.Requisites;
                RelatedDocflowsCount = description.RelatedDocflowsCount;
                SupportRecognition = description.SupportRecognition;
                EncryptedCertificates = description.EncryptedCertificates;
                SupportPrint = description.SupportPrint;
            }

            public DocflowDocumentDescription ConvertToDto() => new()
            {
                Type = Type,
                Filename = Filename,
                ContentType = ContentType,
                DecryptedContentSize = DecryptedContentSize,
                EncryptedContentSize = EncryptedContentSize,
                Compressed = Compressed,
                Requisites = (DocflowDocumentRequisites) (object) Requisites,
                RelatedDocflowsCount = RelatedDocflowsCount,
                SupportRecognition = SupportRecognition,
                EncryptedCertificates = EncryptedCertificates,
                SupportPrint = SupportPrint
            };
        }
    }
}