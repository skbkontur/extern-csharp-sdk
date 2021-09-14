using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Contents
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class ContentResponse
    {
        /// <summary>
        /// Идентификатор контента
        /// </summary>
        public Guid Id { get; set; }
    }
}