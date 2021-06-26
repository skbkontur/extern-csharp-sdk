using System;
using System.Threading.Tasks;
using Kontur.Extern.Client.Concept;
using Kontur.Extern.Client.Models.Organizations;

namespace Kontur.Extern.Client
{
    internal interface IOrganizationListContext : IExtendableContext<AccountPath>
    {
        Task<Organization> CreateAsync(string inn, string kpp, string name);

        /// <summary>
        /// example of entity list with filters
        /// </summary>
        /// <param name="inn"></param>
        /// <param name="kpp"></param>
        /// <returns></returns>
        IEntityList<Organization> List(string inn = null, string kpp = null);

        IOrganizationContext WithId(Guid createdOrganizationId);
    }

    internal interface IOrganizationContext : IExtendableContext<OrganizationPath>
    {
        Task<Organization> GetAsync();

        // NOTE: update method only allows to rename organization, so the name of the method should not be generic Update
        Task Rename(string name);
        Task DeleteAsync();
    }
}