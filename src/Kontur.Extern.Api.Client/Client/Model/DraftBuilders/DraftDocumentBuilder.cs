using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.DraftBuilders.DocumentFiles;
using Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Drafts.Documents.PutDocumentRequestBuilders;
using Kontur.Extern.Api.Client.Cryptography;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Model.Documents.Contents;
using Kontur.Extern.Api.Client.Models.DraftsBuilders.DocumentFiles.Data;
using Kontur.Extern.Api.Client.Uploading;

namespace Kontur.Extern.Api.Client.Model.DraftBuilders
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    [PublicAPI]
    public class DraftsBuilderDocumentFile
    {
        public static SpecifyId FnsInventoryDraftsBuilder(FnsInventoryDraftsBuilderDocumentFileData data) => new(data ?? throw new ArgumentNullException(nameof(data)));
        
        public static SpecifyId FnsLetterDraftsBuilder() => new();
        
        public static SpecifyId RosstatLetterDraftsBuilder() => new();
        
        public static SpecifyId BusinessRegistrationDraftsBuilder(BusinessRegistrationDraftsBuilderDocumentFileData data) => new(data ?? throw new ArgumentNullException(nameof(data)));
        
        public static SpecifyId BusinessRegistrationDraftsBuilderLegacy(BusinessRegistrationDraftsBuilderDocumentFileData data) => new(data ?? throw new ArgumentNullException(nameof(data)));
        
        public static SpecifyId PfrReportDraftsBuilder(PfrReportDraftsBuilderDocumentFileData data) => new(data ?? throw new ArgumentNullException(nameof(data)));
        
        public static SpecifyId PfrIosDraftsBuilder() => new();

        public static SpecifyId PfrLetterDraftsBuilder() => new();
        
        private readonly string fileName;
        private readonly IDocumentContentUploadStrategy uploadStrategy;
        private readonly DraftsBuilderDocumentFileData? data;

        private DraftsBuilderDocumentFile(Guid fileId, string fileName, DraftsBuilderDocumentFileData? data, IDocumentContentUploadStrategy uploadStrategy)
        {
            FileId = fileId;
            this.fileName = fileName;
            this.uploadStrategy = uploadStrategy;
            this.data = data;
        }
        
        public Guid FileId { get; }

        public ValueTask<DraftsBuilderFileRequest> CreateSignedRequestAsync(
            Guid accountId,
            IContentService uploader,
            ICrypt crypt,
            TimeSpan? uploadTimeout)
        {
            return DocumentFileRequestFactory
                .CreateRequestAsync(accountId, fileName, data, uploadStrategy, uploader, crypt, uploadTimeout);
        }

        public class SpecifyId
        {
            private readonly DraftsBuilderDocumentFileData? data;

            internal SpecifyId(DraftsBuilderDocumentFileData? data = null) => this.data = data;

            public SpecifyFile WithNewId() => new(Guid.NewGuid(), data);
        
            public SpecifyFile WithId(Guid documentId) => new(documentId, data);
        }

        public class SpecifyFile
        {
            private readonly Guid documentId;
            private readonly DraftsBuilderDocumentFileData? data;

            internal SpecifyFile(Guid documentId, DraftsBuilderDocumentFileData? data)
            {
                this.documentId = documentId;
                this.data = data;
            }

            public SpecifyContent WithFileName(string fileName)
            {
                if (string.IsNullOrWhiteSpace(fileName))
                    throw Errors.StringsCannotContainNullOrWhitespace(nameof(fileName));
                
                return new(documentId, fileName, data);
            }
        }

        public class SpecifyContent
        {
            private readonly Guid fileId;
            private readonly string fileName;
            private readonly DraftsBuilderDocumentFileData? data;

            internal SpecifyContent(Guid fileId, string fileName, DraftsBuilderDocumentFileData? data)
            {
                this.fileId = fileId;
                this.fileName = fileName;
                this.data = data;
            }

            public SpecifyDocumentSignMethod WithContentToUpload(IDocumentContent documentContent) => new(
                fileId,
                fileName,
                data,
                documentContent ?? throw new ArgumentNullException(nameof(documentContent))
            );

            public DraftsBuilderDocumentFile CreateWithUploadedContent(Guid contentId, Signature? signature = null)
            {
                return new(
                    fileId,
                    fileName,
                    data,
                    new AlreadyUploadedContent(contentId, null, signature)
                );
            }
        }

        public class SpecifyDocumentSignMethod
        {
            private readonly IDocumentContent documentContent;
            private readonly Guid fileId;
            private readonly string fileName;
            private readonly DraftsBuilderDocumentFileData? data;

            internal SpecifyDocumentSignMethod(Guid fileId, string fileName, DraftsBuilderDocumentFileData? data, IDocumentContent documentContent)
            {
                this.fileId = fileId;
                this.fileName = fileName;
                this.data = data;
                this.documentContent = documentContent ?? throw new ArgumentNullException(nameof(documentContent));
            }

            public DraftsBuilderDocumentFile CreateWithCertificate(CertificateContent certificate)
            {
                return new DraftsBuilderDocumentFile(
                    fileId,
                    fileName,
                    data,
                    new DocumentContentToUpload(documentContent, certificate ?? throw new ArgumentNullException(nameof(certificate)))
                );
            }

            public DraftsBuilderDocumentFile CreateWithSignature(Signature signature)
            {
                return new DraftsBuilderDocumentFile(
                    fileId,
                    fileName,
                    data,
                    new DocumentContentToUpload(documentContent, signature ?? throw new ArgumentNullException(nameof(signature)))
                );
            }

            public DraftsBuilderDocumentFile CreateWithoutSignature()
            {
                return new DraftsBuilderDocumentFile(
                    fileId,
                    fileName,
                    data,
                    new DocumentContentToUpload(documentContent)
                );
            }
        }
    }
}