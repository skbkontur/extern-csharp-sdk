using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public enum DraftsBuilderStatus
    {
        New = 0,
        Building,
        Finished
    }
}