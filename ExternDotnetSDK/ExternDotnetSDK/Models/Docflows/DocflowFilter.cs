using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using KeApiOpenSdk.Models.JsonConverters;

namespace KeApiOpenSdk.Models.Docflows
{
    [PublicAPI]
    public class DocflowFilter
    {
        private static PropertyInfo[] properties = typeof (DocflowFilter).GetProperties();

        public bool? Finished { get; set; }
        public bool? Incoming { get; set; }
        public long Skip { get; set; }
        public int Take { get; set; } = 1000;
        /// <summary>
        ///     Get docflows with specified inn-kpp. Supported formats: '1234567890-123456789' (for legal entity),
        ///     '123456789012' (for individual entrepreneur, shouldn't start with '00')
        /// </summary>
        public string InnKpp { get; set; }
        public Guid? OrgId { get; set; }
        public SortOrder? OrderBy { get; set; }
        /// <summary name="updatedFrom">
        ///     Get docflows updated from specified date. Supported formats: ISO 8601
        ///     ("yyyy-mm-ddThh:mm:ss±hh:mm", "yyyy-mm-ddThh:mm:ssZ", "yyyy-mm-ddThh:mm:ss.fffffff±hh:mm" or
        ///     "yyyy-mm-ddThh:mm:ss.fffffffZ")
        /// </summary>
        public DateTime? UpdatedFrom { get; set; }
        public DateTime? UpdatedTo { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public string Type { get; set; }
        public string Knd { get; set; }
        public string Okud { get; set; }
        public string Okpo { get; set; }
        public string Cu { get; set; }
        public string RegNumber { get; set; }
        public string FormName { get; set; }
        public DateTime? PeriodFrom { get; set; }
        public DateTime? PeriodTo { get; set; }

        public Dictionary<string, object> ConvertToQueryParameters()
        {
            var result = new Dictionary<string, object>();
            foreach (var info in properties.Where(x => x.GetValue(this) != null))
                result[ToLowerCamelCase(info.Name)] = info.PropertyType == typeof (DateTime?)
                    ? (info.GetValue(this) as DateTime?)?.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK")
                    : info.GetValue(this)?.ToString();
            return result;
        }

        private string ToLowerCamelCase(string value) => char.ToLowerInvariant(value[0]) + value.Substring(1);
    }
}