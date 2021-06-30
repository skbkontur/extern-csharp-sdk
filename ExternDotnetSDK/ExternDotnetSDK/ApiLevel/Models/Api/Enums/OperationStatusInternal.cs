namespace Kontur.Extern.Client.ApiLevel.Models.Api.Enums
{
    public enum OperationStatusInternal
    {
        Enqueued,
        InProgress,
        Completed,
        CanceledByUser,
        Timeout,
        Crashed,
        UserHasUnconfirmedOperation,
        AwaitingForConfirmation,
        ExceededConfirmationAttemptsCount
    }
}