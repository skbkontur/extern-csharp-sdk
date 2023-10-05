using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Common.Enums;

[PublicAPI]
public enum ReceiptStatus
{
    Unknown = -1,
    Waiting,
    Accepted,
    Rejected,
    Warning,
    AcceptedDeferred,
    DenialAcceptedByCu,
    DenialRejectedByCu
}