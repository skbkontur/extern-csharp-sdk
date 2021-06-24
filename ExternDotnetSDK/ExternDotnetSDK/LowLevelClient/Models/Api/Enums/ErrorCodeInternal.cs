namespace Kontur.Extern.Client.Models.Api.Enums
{
    public enum ErrorCodeInternal
    {
        None,
        Unknown,
        BadEncryptedData,
        EncryptedForOtherRecipient,
        UnknownCertificate
    }
}