#nullable enable
using System.Threading.Tasks;
using Kontur.Extern.Client.Cryptography;

namespace Kontur.Extern.Client.Model.Drafts
{
    public interface IDocumentContent
    {
        string? ContentType { get; }

        Task<Signature> SignAsync(CertificateContent certificate, ICrypt crypt);
    }
}