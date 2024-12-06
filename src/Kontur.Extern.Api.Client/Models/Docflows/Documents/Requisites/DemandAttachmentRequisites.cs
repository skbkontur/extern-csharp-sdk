using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Common.Time;
using Kontur.Extern.Api.Client.Models.Numbers;

namespace Kontur.Extern.Api.Client.Models.Docflows.Documents.Requisites
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class DemandAttachmentRequisites : DocflowDocumentRequisites
    {
        /// <summary>
        /// Номер требования
        /// </summary>
        //[Required]
        public string DemandNumber { get; set; } = null!;

        /// <summary>
        /// КНД распознанного поручения
        /// </summary>
        public Knd? DemandKnd { get; set; }

        /// <summary>
        /// Дата из требования
        /// </summary>
        public DateOnly? DemandDate { get; set; }

        /// <summary>
        /// Дедлайн отправки квитанции на требование
        /// </summary>
        public DateOnly? ReceiptDeadlineDate { get; set; }

        /// <summary>
        /// Дедлайн ответа на требование
        /// </summary>
        public DateOnly? ReplyDeadlineDate { get; set; }

        /// <summary>
        /// Список ИНН, содержащихся в требовании
        /// </summary>
        public string[] DemandInnList { get; set; } = null!;

        /// <summary>
        /// Наименование налогового органа
        /// </summary>
        public string? TaxDepartmentName { get; set; }

        /// <summary>
        /// Наименование мероприятия налогового контроля
        /// </summary>
        public string? TaxEventName { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public Comment? Comment { get; set; }

        /// <summary>
        /// Метки
        /// </summary>
        public List<Label>? Labels { get; set; }

        /// <summary>
        /// Список ответственных
        /// <remarks>На данный момент список может быть либо пустым, либо состоять из одного ответственного</remarks>
        /// </summary>
        public List<Assignee> Assignees { get; set; } = null!;

        /// <summary>
        /// Статус требования
        /// </summary>
        public DemandStatus DemandStatus { get; set; }
    }

    public class Comment
    {
        /// <summary>
        /// Текст комментария
        /// </summary>
        public string Text { get; set; } = null!;

        /// <summary>
        /// Цвет комментария для веб-интерфейса Контур.Экстерна
        /// </summary>
        public CommentColor Color { get; set; }
    }

    public enum CommentColor
    {
        None,
        Red,
        Violet,
        Gray,
        Yellow,
        Green,
        Blue
    }

    public class Label
    {
        /// <summary>
        /// Текст метки
        /// </summary>
        public string Name { get; set; } = null!;
    }

    public class Assignee
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string? MiddleName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string? LastName { get; set; }
    }

    public enum DemandStatus
    {
        /// <summary>
        /// В работе
        /// </summary>
        InProgress,
        /// <summary>
        /// Выполнено
        /// </summary>
        Done,
    }
}