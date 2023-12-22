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
        public RegistrationInfo(ApplicantInfo[] applicantInfos, IpInfo? ipInfo, UlInfo? ulInfo, ApplicationCode? applicationCode)
        {
            if (ipInfo != null && ulInfo != null)
            {
                throw Errors.UlInfoAndIpInfoAreFilledAtTheSameTime();
            }

            if (ipInfo == null && ulInfo == null)
            {
                throw Errors.JsonDoesNotContainOneOfProperties(new[] {nameof(ipInfo), nameof(ulInfo)});
            }

            if (ulInfo != null)
            {
                if (applicationCode is not null && applicationCode.Value.IsIndividualEntrepreneur())
                    throw Errors.WrongApplicationCodeForBusinessRegistrationType(BusinessType.Ul, applicationCode.Value);
            }
            else if (ipInfo != null)
            {
                if (applicationCode is not null && !applicationCode.Value.IsIndividualEntrepreneur())
                    throw Errors.WrongApplicationCodeForBusinessRegistrationType(BusinessType.Ip, applicationCode.Value);
            }

            ApplicantInfos = applicantInfos;
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
        /// Информация об ИП
        /// </summary>
        public IpInfo? IpInfo { get; }

        /// <summary>
        /// Информация о ЮЛ
        /// </summary>
        public UlInfo? UlInfo { get; }
    }
}