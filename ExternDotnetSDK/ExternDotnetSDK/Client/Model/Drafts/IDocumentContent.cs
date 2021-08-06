#nullable enable
using System.Threading.Tasks;

namespace Kontur.Extern.Client.Model.Drafts
{
    public interface IDocumentContent
    {
        string? ContentType { get; }
        Task<byte[]> GetBytesAsync();
    }
}