using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Client.Concept;
using Kontur.Extern.Client.Models.Accounts;
using Kontur.Extern.Client.Models.Certificates;
using Kontur.Extern.Client.Models.Warrants;

namespace Kontur.Extern.Client
{
    interface IAccountListContext : IExtendableContext
    {
        Task<Account> CreateAsync(string inn, string kpp, string organizationName);

        IEntityList<Account> List();

        Task<List<Account>> FindAsync(string inn, string kpp);

        IAccountContext WithId(Guid accountId);
    }

    interface IAccountContext : IExtendableContext<AccountPath>
    {
        Task DeleteAsync();

        Task<Account> GetAsync();

        /// <summary>
        /// property style entities list example.
        /// </summary>
        IEntityList<CertificateDto> Certificates { get; }

        /// <summary>
        /// method style entities list example.
        /// allows to use warrants with exposed default page size in signature
        /// </summary>
        /// <returns></returns>
        IEntityList<Warrant> Warrants { get; }
        
        IOrganizationListContext Organizations { get; }
        IDocflowListContext Docflows { get; }
    }
}