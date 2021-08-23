using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.Model.Docflows
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    public partial struct DocflowType
    {
        /// <summary>
        /// Росстат
        /// </summary>
        [PublicAPI]
        public static class Rosstat
        {
            /// <summary>
            /// Тип документооборота: Отчет
            /// Документооборот по предоставлению отчетности в органы ФСГС
            /// </summary>
            public static readonly DocflowType Report = "urn:docflow:stat-report";

            /// <summary>
            /// Тип документооборота: Письмо в Росстат
            /// Документооборот по осуществлению письменных обращений респондентов в орган ФСГС
            /// </summary>
            public static readonly DocflowType Letter = "urn:docflow:stat-letter";

            /// <summary>
            /// Тип документооборота: Письмо из Росстата
            /// Документооборот по осуществлению индивидуального информирования респондентов со стороны органов ФСГС
            /// </summary>
            public static readonly DocflowType CuLetter = "urn:docflow:stat-cu-letter";

            /// <summary>
            /// Тип документооборота: Массовая рассылка из Росстата
            /// Документооборот по осуществлению информационной рассылки со стороны налоговых органов
            /// </summary>
            public static readonly DocflowType CuBroadcast = "urn:docflow:stat-cu-broadcast";
        }
    }
}