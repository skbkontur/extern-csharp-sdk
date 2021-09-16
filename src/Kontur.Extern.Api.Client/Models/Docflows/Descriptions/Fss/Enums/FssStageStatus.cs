using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fss.Enums
{
    /// <summary>
    /// Статусы стадий документооборота расчет 4-ФСС
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public enum FssStageStatus
    {
        /// <summary>
        /// Успех. Стадия завершилась успешно
        /// </summary>
        Success,
        
        /// <summary>
        /// Ошибка. Стадия завершился неудачей
        /// </summary>
        Error	
    }
}