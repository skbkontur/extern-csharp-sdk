using JetBrains.Annotations;

namespace ExternDotnetSDK.Models.Warrants
{
    /// <summary>
    ///     Тип представителя
    /// </summary>
    [PublicAPI]
    public enum TrustedIssuerType
    {
        /// <summary>
        ///     Законный представитель - тот, кто по закону по умолчанию имеет права (например, опекун или конкурсный управляющий).
        /// </summary>
        Legal = 1,

        /// <summary>
        ///     Уполномоченный представитель - лицо, на которое дали доверенность (в свою очередь, может выдать доверенность
        ///     другому лицу)
        /// </summary>
        Authorized = 2
    }
}