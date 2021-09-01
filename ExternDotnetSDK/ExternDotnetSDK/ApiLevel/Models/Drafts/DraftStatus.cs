using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Drafts
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