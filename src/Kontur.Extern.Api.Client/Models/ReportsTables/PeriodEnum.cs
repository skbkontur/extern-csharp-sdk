using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.ReportsTables;

[Flags]
[PublicAPI]
[SuppressMessage("ReSharper", "CommentTypo")]
public enum PeriodEnum
{
    /// <summary>
    /// Ежегодный
    /// </summary>
    Yearly = 1,

    /// <summary>
    /// Ежеквартальный
    /// </summary>
    Quarterly = 2,

    /// <summary>
    /// Ежемесячный
    /// </summary>
    Monthly = 4,
}