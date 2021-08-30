using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Client.ApiLevel.Models.Common;

namespace Kontur.Extern.Client.ApiLevel.Models.Accounts
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class Account
    {
        /// <summary>
        /// Идентификатор учетной записи
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// ИНН головной организации 
        /// </summary>
        public string Inn { get; set; }
        /// <summary>
        /// КПП головной организации
        /// </summary>
        public string Kpp { get; set; }
        /// <summary>
        /// Название организации
        /// </summary>
        public string OrganizationName { get; set; }
        /// <summary>
        /// Продукт, с которым может работать пользователь
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Роль пользователя в данной учетной записи: администратор, пользователь, директор
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// Ссылки для работы с учетной записью
        /// </summary>
        public Link[] Links { get; set; }
    }
}