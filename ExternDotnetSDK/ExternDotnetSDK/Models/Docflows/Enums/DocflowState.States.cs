using System.Diagnostics.CodeAnalysis;

namespace Kontur.Extern.Client.Models.Docflows.Enums
{
    [SuppressMessage("ReSharper", "CommentTypo")]
    partial struct DocflowState
    {
            /// <summary>
            /// Состояние не определенно. Отчет ещё находится в обработке (не пришла финальная квитанция). Необходимо отслеживать документооборот, пока он не перейдет в одно из следующих состояний.
            /// </summary>
            public static readonly DocflowState Neutral = "urn:docflow-state:neutral"; 
            
            /// <summary>
            /// Успешно обработан. Пришел положительный протокол, необходимо продолжить документооброт согласно Статусам и порядку документооборота.
            /// </summary>
            public static readonly DocflowState Successful = "urn:docflow-state:successful"; 
            
            /// <summary>
            /// Обработка завершилась ошибкой или отказом. Нужно ознакомиться с ошибками и переотправить отчет.
            /// </summary>
            public static readonly DocflowState Failed = "urn:docflow-state:failed"; 
            
            /// <summary>
            /// Обработка завершилась успешно, но у контролирующего органа есть претензии. Пришло уведомление об уточнение. нужно изучить протокол и при необходимости направить корректировку
            /// </summary>
            public static readonly DocflowState Warning = "urn:docflow-state:warning"; 
    }
}