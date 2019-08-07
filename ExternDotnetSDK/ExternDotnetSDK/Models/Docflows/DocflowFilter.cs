using System;
using System.Linq;
using System.Reflection;
using ExternDotnetSDK.Models.JsonConverters;
using JetBrains.Annotations;

namespace ExternDotnetSDK.Models.Docflows
{
    [PublicAPI]
    public class DocflowFilter
    {
        private static PropertyInfo[] properties = typeof (DocflowFilter).GetProperties();
        /// <summary>Get finished docflow</summary>
        public bool? Finished { get; set; }

        /// <summary>Get incoming docflows</summary>
        public bool? Incoming { get; set; }

        /// <summary>Get docflows with skip elements</summary>
        public long Skip { get; set; }

        /// <summary>Get take docflows</summary>
        public int Take { get; set; } = 1000;

        /// <summary>
        ///     Get docflows with specified inn-kpp. Supported formats: '1234567890-123456789' (for legal entity),
        ///     '123456789012' (for individual entrepreneur, shouldn't start with '00')
        /// </summary>
        public string InnKpp { get; set; }

        /// <summary name="orgId">Get docflows with specified organization identifier</summary>
        public Guid? OrgId { get; set; }

        /// <summary name="orderBy">Get docflows sorted by ascending/descending of creation date</summary>
        public SortOrder? OrderBy { get; set; }

        /// <summary name="updatedFrom">
        ///     Get docflows updated from specified date. Supported formats: ISO 8601
        ///     ("yyyy-mm-ddThh:mm:ss±hh:mm", "yyyy-mm-ddThh:mm:ssZ", "yyyy-mm-ddThh:mm:ss.fffffff±hh:mm" or
        ///     "yyyy-mm-ddThh:mm:ss.fffffffZ")
        /// </summary>
        public DateTime? UpdatedFrom { get; set; }

        /// <summary name="updatedTo">Get docflows updated to specified date. Supported formats are the same as for "updatedFrom"</summary>
        public DateTime? UpdatedTo { get; set; }

        /// <summary name="createdFrom">
        ///     Get docflows created from specified date. Supported formats are the same as for
        ///     "updatedFrom"
        /// </summary>
        public DateTime? CreatedFrom { get; set; }

        /// <summary name="createdTo">Get docflows created to specified date. Supported formats are the same as for "updatedFrom"</summary>
        public DateTime? CreatedTo { get; set; }

        /// <summary name="type">Get docflows with specified type</summary>
        public string Type { get; set; }

        /// <summary name="knd">Get docflows with specified knd</summary>
        public string Knd { get; set; }

        /// <summary name="okud">Get docflows with specified okud</summary>
        public string Okud { get; set; }

        /// <summary name="okpo">Get docflows with specified okpo</summary>
        public string Okpo { get; set; }

        /// <summary name="cu">Get docflows to (from) specified control unit</summary>
        public string Cu { get; set; }

        /// <summary name="regNumber">Get PFR docflows with regNumber</summary>
        public string RegNumber { get; set; }

        /// <summary name="formName">Get docflows with form name contains</summary>
        public string FormName { get; set; }

        /// <summary name="periodFrom">Get docflows with period from specified date</summary>
        public DateTime? PeriodFrom { get; set; }

        /// <summary name="periodTo">Get docflows with period to specified date</summary>
        public DateTime? PeriodTo { get; set; }

        public string StringifyParams()
        {
            var requestParams = properties
                .Select(
                    x => new
                    {
                        x.Name,
                        Value = x.PropertyType == typeof (DateTime?)
                            ? (x.GetValue(this) as DateTime?)?.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                            : x.GetValue(this)?.ToString()
                    })
                .Where(x => !string.IsNullOrEmpty(x.Value))
                .Select(x => $"{ToLowerCamelCase(x.Name)}={x.Value}");
            return requestParams.Any()
                ? "?" + string.Join("&", requestParams)
                : "";
        }

        private string ToLowerCamelCase(string value) => char.ToLowerInvariant(value[0]) + value.Substring(1);
    }
}