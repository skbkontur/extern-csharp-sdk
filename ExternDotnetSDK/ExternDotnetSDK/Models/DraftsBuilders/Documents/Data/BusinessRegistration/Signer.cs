using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.Models.Common;

namespace Kontur.Extern.Client.Models.DraftsBuilders.Documents.Data.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Signer
    {
        /// <summary>
        /// ФИО
        /// </summary>
        public Fio Fio { get; set; }
    }
}