using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Drafts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public enum DraftStatus
    {
        New,
        CheckInProgress,
        Checked,
        PrepareInProgress,
        ReadyToSend,
        SendInProgress,
        Sent
    }
}