using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss.Enums;

/// <summary>
/// Типы операции с доверенностью FssWarrantManagement.
/// </summary>
[PublicAPI]
public class WarrantManagementActionType
{
    /// <summary>
    /// Неизвестный тип
    /// </summary>
    public static readonly string Unknown = "unknown";

    /// <summary>
    /// Запрос на создание доверенности
    /// </summary>
    public static readonly string Issuance = "issuance";

    /// <summary>
    /// Запрос на отзыв доверенности
    /// </summary>
    public static readonly string Revocation = "revocation";
}