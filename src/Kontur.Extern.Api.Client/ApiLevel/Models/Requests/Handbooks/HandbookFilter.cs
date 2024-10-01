using JetBrains.Annotations;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Requests.Handbooks;

[PublicAPI]
public class HandbookFilter
{
    public string[]? Types { get; set; }
    public string[]? Regions { get; set; }
}