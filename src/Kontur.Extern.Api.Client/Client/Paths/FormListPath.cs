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
        this.services = services ?? throw new ArgumentNullException(nameof(services));
    }

    public Guid AccountId { get; }
    public Guid OrganizationId { get; }
    private readonly IExternClientServices services;

    #region ObsoleteCode
    [Obsolete($"Use {nameof(IExtern)}.{nameof(IExtern.Services)} instead")]
    public IExternClientServices Services => services;
    #endregion

    public async Task<IReadOnlyCollection<FormInfo>> ListAsync(bool? includeDeleted = false, TimeSpan? timeout = null)
    {
        var apiClient = services.Api;
        var formsList = await apiClient.ReportsTables.GetFormsAsync(AccountId, OrganizationId, includeDeleted, timeout).ConfigureAwait(false);
        return formsList.Forms;
    }
}