using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Accounts
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
        public string Inn { get; set; } = null!;
        /// <summary>
        /// КПП головной организации
        /// </summary>
        public Kpp? Kpp { get; set; }
        /// <summary>
        /// Название организации
        /// </summary>
        public string OrganizationName { get; set; } = null!;
        /// <summary>
        /// Продукт, с которым может работать пользователь
        /// </summary>
        [Obsolete("use Products")]
        public string ProductName { get; set; } = null!;

        /// <summary>
        /// Список продуктов Контура
        /// </summary>
        public Product[] Products { get; set; } = null!;

        /// <summary>
        /// Роль пользователя в данной учетной записи: администратор, пользователь, директор
        /// </summary>
        public string Role { get; set; } = null!;
        /// <summary>
        /// Ссылки для работы с учетной записью
        /// </summary>
        public Link[] Links { get; set; } = null!;

        /// <summary>
        /// Идентификатор группы учетных записей
        /// </summary>
        public Guid? AbonentId { get; set; }
    }
}