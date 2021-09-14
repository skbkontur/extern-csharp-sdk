using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Models.Common;
using Kontur.Extern.Api.Client.Models.Warrants;

namespace Kontur.Extern.Api.Client.ApiLevel.Models.Responses.Warrants
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class WarrantList
    {
        public long Skip { get; set; }
        public long Take { get; set; }
        public long TotalCount { get; set; }
        public Warrant?[] Warrants { get; set; }
        public OrganizationWarrantInformation[] OrganizationWarrantInformations { get; set; }
        public Link[] Links { get; set; }
    }
}