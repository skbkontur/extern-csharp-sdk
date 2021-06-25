using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Clients.Docflows;
using Kontur.Extern.Client.Models.Accounts;
using Kontur.Extern.Client.Models.Certificates;
using Kontur.Extern.Client.Models.Warrants;

namespace Kontur.Extern.Client
{
    interface IAccountListContext
    {
        Task<Account> CreateAsync(string inn, string kpp, string organizationName);

        IEntityList<Account> List(uint take = 100);
        IAccountContext WithId(Guid accountId);
    }

    interface IAccountContext
    {
        Task DeleteAsync();

        Task<Account> GetAsync();

        IPagination<CertificateDto> GetCertificates();

        /// <summary>
        /// property style entities list example.
        /// </summary>
        IEntityList<CertificateDto> Certificates { get; }

        /// <summary>
        /// method style entities list example.
        /// allows to use warrants with exposed default page size in signature
        /// </summary>
        /// <param name="takeSize"></param>
        /// <returns></returns>
        IEntityList<Warrant> Warrants(int take = 100);
        
        IOrganizationListContext Organizations { get; }
        IDocflowListContext Docflows { get; }
    }
}