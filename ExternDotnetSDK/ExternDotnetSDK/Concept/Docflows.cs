using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Models.Documents;

namespace Kontur.Extern.Client
{
    internal interface IDocflowListContext
    {
        IDocflowContext WithId(Guid docflowId);
    }

    internal interface IDocflowContext
    {
        IDocumentListContext Documents { get; }
    }

    internal interface IDocumentListContext
    {
        IDocumentContext WithId(Guid documentId);
    }

    internal interface IDocumentContext
    {
        ILongOperation DssDecrypt { get; }

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