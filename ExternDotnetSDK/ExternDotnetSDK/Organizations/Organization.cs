using System;

namespace ExternDotnetSDK.Organizations
{
    public class Organization
    {
        public Guid Id { get; set; }
        public OrganizationGeneral General { get; set; }
    }
}