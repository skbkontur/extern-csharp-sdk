using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Kontur.Extern.Api.Client.Exceptions;
using Kontur.Extern.Api.Client.Models.Docflows.Descriptions.Fns.BusinessRegistration;

namespace Kontur.Extern.Api.Client.Models.DraftsBuilders.Builders.Data.BusinessRegistration
{
    [PublicAPI]
    [SuppressMessage("ReSharper", "CommentTypo")]
    public class RegistrationInfo
    {
        public RegistrationInfo(ApplicantInfo[] applicantInfos, BusinessType? businessType, IpInfo? ipInfo, UlInfo? ulInfo, ApplicationCode? applicationCode)
        {
            if (businessType == Docflows.Descriptions.Fns.BusinessRegistration.BusinessType.Ul)
            {
                if (ulInfo == null)
                    throw Errors.JsonPropertyIsMissedButRequiredBecauseOfOtherHaveValue(nameof(ulInfo), nameof(businessType), businessType);

                if (applicationCode is not null && applicationCode.Value.IsIndividualEntrepreneur())
                    throw Errors.WrongApplicationCodeForBusinessRegistrationType(businessType.Value, applicationCode.Value);
            }
            else if (businessType == Docflows.Descriptions.Fns.BusinessRegistration.BusinessType.Ip)
            {
                if (ipInfo == null)
                    throw Errors.JsonPropertyIsMissedButRequiredBecauseOfOtherHaveValue(nameof(ipInfo), nameof(businessType), businessType);

                if (applicationCode is not null && !applicationCode.Value.IsIndividualEntrepreneur())
                    throw Errors.WrongApplicationCodeForBusinessRegistrationType(businessType.Value, applicationCode.Value);
            }
            else
            {
                if (ipInfo != null || ulInfo != null || applicationCode != null)
                    throw Errors.UnknownBusinessTypeCannotHaveParticularData();
            }

            ApplicantInfos = applicantInfos;
            BusinessType = businessType;
            IpInfo = ipInfo;
            UlInfo = ulInfo;
            ApplicationCode = applicationCode;
        }

        /// <summary>
        /// Код заявления по справочнику СФРД
        /// </summary>
        public ApplicationCode? ApplicationCode { get; }

        /// <summary>
        /// Информация о заявителе
        /// </summary>
        //[Required]
        public ApplicantInfo[] ApplicantInfos { get; }

        /// <summary>
        /// Тип регистрируемого бизнеса
        /// </summary>
        public BusinessType? BusinessType { get; }
        
        /// <summary>
        /// Информация об ИП
        /// </summary>
        public IpInfo? IpInfo { get; }
        
        /// <summary>
        /// Информация о ЮЛ
        /// </summary>
        public UlInfo? UlInfo { get; }
    }
}