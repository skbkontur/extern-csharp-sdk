using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.ReportsTables;

[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public enum PeriodEnum
{
    /// <summary>
    /// Ежегодный
    /// </summary>
    Yearly,

    /// <summary>
    /// Ежеквартальный
    /// </summary>
    Quarterly,

    /// <summary>
    /// Ежемесячный
    /// </summary>
    Monthly,
}