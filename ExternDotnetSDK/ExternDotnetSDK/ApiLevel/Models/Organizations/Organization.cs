﻿using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Client.ApiLevel.Models.Organizations
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Organization
    {
        /// <summary>
        /// Идентификатор организации
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Общие данные по организации
        /// </summary>
        public OrganizationGeneral General { get; set; }
    }
}