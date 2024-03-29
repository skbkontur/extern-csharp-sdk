﻿using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Drafts.Meta
{
    /// <summary>
    /// Учетная запись организации
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class AccountInfo
    {
        private OrganizationInfo organization;

        public AccountInfo() => organization = new OrganizationInfo();

        /// <summary>
        ///     ИНН
        /// </summary>
        //[Required]
        public string Inn { get; set; } = null!;

        /// <summary>
        /// Название
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Данные о ЮЛ
        /// </summary>
        public OrganizationInfo Organization
        {
            get => organization;
            // ReSharper disable once ConstantNullCoalescingCondition
            set => organization = value ?? new OrganizationInfo();
        }

        /// <summary>
        /// Регистрационный номер ФСС
        /// </summary>
        public FssRegNumber? RegistrationNumberFss { get; set; }

        /// <summary>
        /// Регистрационный номер ПФР
        /// </summary>
        public PfrRegNumber? RegistrationNumberPfr { get; set; }

        /// <summary>
        /// ОКПО (для писем в росстат)
        /// </summary>
        public Okpo Okpo { get; set; } = null!;
    }
}