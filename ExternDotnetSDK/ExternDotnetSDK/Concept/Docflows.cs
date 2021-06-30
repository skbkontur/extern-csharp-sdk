using System;

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
    }
}