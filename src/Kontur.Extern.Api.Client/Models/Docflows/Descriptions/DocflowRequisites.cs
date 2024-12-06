using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.Models.Docflows.Descriptions;

[PublicAPI]
public class DocflowRequisites
{
    /// <summary>
    /// Комментарий к документообороту
    /// </summary>
    public Comment? Comment { get; set; }
}

[PublicAPI]
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