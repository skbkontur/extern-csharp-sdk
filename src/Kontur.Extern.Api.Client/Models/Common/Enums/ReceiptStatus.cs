using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Common.Enums;

[PublicAPI]
public enum ReceiptStatus
{
    Waiting,
    Accepted,
    Rejected,
    Warning,
    AcceptedDeferred,
}