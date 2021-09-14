using System;
using System.IO;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.End2EndTests.Client.TestContext
{
    internal class ContentsContext
    {
        private readonly IExtern konturExtern;

        public ContentsContext(IExtern konturExtern) => this.konturExtern = konturExtern;

        public Task<Stream> GetContentStream(Guid accountId, Guid contentId) => 
            konturExtern.Accounts.WithId(accountId).Contents.DownloadAsStreamAsync(contentId);

        public Task<Guid> UploadAsync(Guid accountId, Stream stream) => 
            konturExtern.Accounts.WithId(accountId).Contents.UploadAsync(stream);
    }
}