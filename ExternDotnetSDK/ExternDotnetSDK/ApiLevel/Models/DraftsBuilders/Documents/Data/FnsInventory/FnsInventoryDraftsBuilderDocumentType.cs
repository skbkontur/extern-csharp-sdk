﻿using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Documents.Data.FnsInventory
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