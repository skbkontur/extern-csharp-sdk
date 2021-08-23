using System.Diagnostics.CodeAnalysis;

namespace Kontur.Extern.Client.Model.Docflows
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    public partial struct DocflowType
    {
        /// <summary>
        /// Отчетность в ЦБ РФ
        /// </summary>
        public static class Cbrf 
        {
            /// <summary>
            /// Отчетность в ЦБ РФ
            /// </summary>
            public static readonly DocflowType Report = "urn:docflow:cbrf-report";
        }
    }
}