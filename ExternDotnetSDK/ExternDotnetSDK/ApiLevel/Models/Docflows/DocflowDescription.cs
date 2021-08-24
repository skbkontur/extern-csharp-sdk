using System;
using Kontur.Extern.Client.ApiLevel.Models.JsonConverters;
using Newtonsoft.Json;
// ReSharper disable CommentTypo

namespace Kontur.Extern.Client.ApiLevel.Models.Docflows
{
    [JsonObject(NamingStrategyType = typeof (KebabCaseNamingStrategy))]
    public abstract class DocflowDescription
    {
        /// <summary>
        /// Идентификатор черновика документооборота, если он был создан через API
        /// </summary>
        public Guid? OriginalDraftId { get; set; }
    }
}