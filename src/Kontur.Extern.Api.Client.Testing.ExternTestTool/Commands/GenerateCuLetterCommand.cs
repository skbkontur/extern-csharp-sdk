using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Kontur.Extern.Api.Client.Models.Docflows;
using Kontur.Extern.Api.Client.Testing.ExternTestTool.Http;
using Kontur.Extern.Api.Client.Testing.ExternTestTool.Models.Requests;
using Kontur.Extern.Api.Client.Testing.ExternTestTool.ResponseCaching;

namespace Kontur.Extern.Api.Client.Testing.ExternTestTool.Commands
{
    internal class GenerateCuLetterCommand : IExternTestToolCommand<IDocflowWithDocuments>
    {
        private readonly GenerateCuLetterRequest request;

        public GenerateCuLetterCommand(Guid accountId, Sender? sender, Payer? payer, string? textOfLetter, TestIfnsCode? ifnsCode) => 
            request = new GenerateCuLetterRequest(accountId, sender, payer, textOfLetter, ifnsCode?.ToString());

        public async Task<IDocflowWithDocuments> ExecuteAsync(IHttpClient httpClient, IResponseCache cache)
        {
            var responseMessage = await httpClient.PostAsJsonAsync("generate-cu-letter", request);
            return (await responseMessage.Content.ReadFromJsonAsync<IDocflowWithDocuments>())!;
        }

        private record GenerateCuLetterRequest(Guid AccountId, Sender? Sender, Payer? Payer, string? TextOfLetter, string? IfnsCode);
    }
}