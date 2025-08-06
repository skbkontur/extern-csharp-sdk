using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Attributes;
using Kontur.Extern.Api.Client.Common;
using Kontur.Extern.Api.Client.Models.ReportsTables.Forms;

namespace Kontur.Extern.Api.Client.Paths;

[PublicAPI]
[ApiPathSection]
public readonly struct FormListPath
{
    public FormListPath(Guid accountId, Guid organizationId, IExternClientServices services)
    {
        AccountId = accountId;
        OrganizationId = organizationId;
        Services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public Guid AccountId { get; }
    public Guid OrganizationId { get; }
    public IExternClientServices Services { get; }

    public async Task<IReadOnlyCollection<FormInfo>> ListAsync(bool? includeDeleted = false, TimeSpan? timeout = null)
    {
        var apiClient = Services.Api;
        var formsList = await apiClient.ReportsTables.GetFormsAsync(AccountId, OrganizationId, includeDeleted, timeout).ConfigureAwait(false);
        return formsList.Forms;
    }
}