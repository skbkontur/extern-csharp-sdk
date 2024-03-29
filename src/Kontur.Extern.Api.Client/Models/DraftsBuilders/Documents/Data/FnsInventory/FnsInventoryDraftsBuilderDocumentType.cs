﻿using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Documents.Data.FnsInventory
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public enum FnsInventoryDraftsBuilderDocumentType
    {
        Formalized,
        Scanned,
        Warrant
    }
}