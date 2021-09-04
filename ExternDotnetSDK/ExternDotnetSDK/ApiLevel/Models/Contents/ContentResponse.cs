﻿using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Contents
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