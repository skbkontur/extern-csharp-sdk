namespace ExternDotnetSDK.DraftsBuilders.DocumentFiles
{
    public class DraftsBuilderDocumentFileContents
    {
        public string Base64Content { get; set; }
        public string Base64SignatureContent { get; set; }
        public DraftsBuilderDocumentFileMetaRequest Meta { get; set; }
    }
}