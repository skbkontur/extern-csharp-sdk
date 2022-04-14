using System.Diagnostics.CodeAnalysis;

namespace Kontur.Extern.Api.Client.Models.Docflows.Enums
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    public partial struct DocflowType
    {
        /// <summary>
        /// Отчетность в ЦБ РФ
        /// </summary>
        public static readonly DocflowType CbrfReport = "urn:docflow:cbrf-report";
    }
}