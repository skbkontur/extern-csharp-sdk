using System.Diagnostics.CodeAnalysis;

namespace Kontur.Extern.Api.Client.Models.Docflows.Enums
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    public partial struct DocflowType
    {
        /// <summary>
        /// Тип документооборота: Отчет
        /// Документооборот по предоставлению отчетности в органы ФСГС
        /// </summary>
        public static readonly DocflowType StatReport = "urn:docflow:stat-report";

        /// <summary>
        /// Тип документооборота: Письмо в Росстат
        /// Документооборот по осуществлению письменных обращений респондентов в орган ФСГС
        /// </summary>
        public static readonly DocflowType StatLetter = "urn:docflow:stat-letter";

        /// <summary>
        /// Тип документооборота: Письмо из Росстата
        /// Документооборот по осуществлению индивидуального информирования респондентов со стороны органов ФСГС
        /// </summary>
        public static readonly DocflowType StatCuLetter = "urn:docflow:stat-cu-letter";

        /// <summary>
        /// Тип документооборота: Массовая рассылка из Росстата
        /// Документооборот по осуществлению информационной рассылки со стороны налоговых органов
        /// </summary>
        public static readonly DocflowType StatCuBroadcast = "urn:docflow:stat-cu-broadcast";
    }
}