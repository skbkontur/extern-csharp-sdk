using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Concept;

namespace Kontur.Extern.Client
{
    internal interface IDocflowListContext : IExtendableContext<AccountPath>
    {
        IDocflowContext WithId(Guid docflowId);
    }

    internal interface IDocflowContext : IExtendableContext<DocflowPath>
    {
        IDocumentListContext Documents { get; }
    }

    internal interface IDocumentListContext : IExtendableContext<DocflowPath>
    {
        IDocumentContext WithId(Guid documentId);
    }

    internal interface IDocumentContext : IExtendableContext<DocumentPath>
    {
        IDeferredOperation DssDecrypt { get; }

        /// <summary>
        /// Check status of the task outside of the IDeferred instance
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns>return status or throw exception of the task or API</returns>
        Task<DecryptDocumentStatus> GetDssDecryptStatusAsync(Guid taskId);
    }

    internal class DecryptDocumentStatus
    {
        public bool IsCompleted { get; }
    }
}