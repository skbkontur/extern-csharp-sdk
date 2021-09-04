﻿using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.DraftsBuilders.Builders.Data.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class UlInfo
    {
        /// <summary>
        /// ОГРН
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public string Ogrn { get; set; }

        /// <summary>
        /// Название организации
        /// </summary>
        // [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }
    }
}