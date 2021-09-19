using System;
using System.Collections.Generic;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration
{
    public static class ApplicationCodeExtension
    {
        private static readonly HashSet<ApplicationCode> IndividualEntrepreneurCodes = GetAllIndividualEntrepreneurCodes();

        public static bool IsIndividualEntrepreneur(this ApplicationCode code) => 
            IndividualEntrepreneurCodes.Contains(code);

        private static HashSet<ApplicationCode> GetAllIndividualEntrepreneurCodes()
        {
            const string individualEntrepreneurPrefix = "Р2";
            
            var individualEntrepreneurCodes = new HashSet<ApplicationCode>();
            foreach (var code in (ApplicationCode[]) Enum.GetValues(typeof (ApplicationCode)))
            {
                if (code.ToString().StartsWith(individualEntrepreneurPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    individualEntrepreneurCodes.Add(code);
                }
            }

            return individualEntrepreneurCodes;
        }
    }
}