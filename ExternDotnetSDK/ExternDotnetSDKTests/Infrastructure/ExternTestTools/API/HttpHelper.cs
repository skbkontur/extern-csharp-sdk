using System;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using RestSharp;

namespace Kontur.Extern.Client.Tests.Infrastructure.ExternTestTools.API
{
    public static class HttpHelper
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
        };

        /// <summary>
        /// Dynamically cast the object into target type.
        /// </summary>
        public static dynamic ConvertType(dynamic fromObject, Type toObject)
        {
            return Convert.ChangeType(fromObject, toObject);
        }

        public static byte[] ReadAsBytes(Stream inputStream)
        {
            var buf = new byte[16*1024];
            using (var ms = new MemoryStream())
            {
                int count;
                while ((count = inputStream.Read(buf, 0, buf.Length)) > 0)
                {
                    ms.Write(buf, 0, count);
                }

                return ms.ToArray();
            }
        }

        /// <summary>
        /// Encode string in base64 format.
        /// </summary>
        /// <param name="text">String to be encoded.</param>
        /// <returns>Encoded string.</returns>
        public static string Base64Encode(string text)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public static string ToJson(object o)
        {
            return JsonConvert.SerializeObject(o, Formatting.Indented);
        }

        public static object Deserialize(IRestResponse response, Type type)
        {
            if (type == typeof (byte[]))
            {
                return response.RawBytes;
            }

            if (type == typeof (Stream))
            {
                return new MemoryStream(response.RawBytes);
            }

            if (type.Name.StartsWith("System.Nullable`1[[System.DateTime"))
            {
                return DateTime.Parse(response.Content, null, DateTimeStyles.RoundtripKind);
            }

            if (type == typeof (string) || type.Name.StartsWith("System.Nullable"))
            {
                return ConvertType(response.Content, type);
            }

            try
            {
                return JsonConvert.DeserializeObject(response.Content, type, SerializerSettings);
            }
            catch (Exception e)
            {
                throw new ApiException(500, e.Message);
            }
        }

        public static string Serialize(object obj)
        {
            try
            {
                return obj != null ? JsonConvert.SerializeObject(obj) : null;
            }
            catch (Exception e)
            {
                throw new ApiException(500, e.Message);
            }
        }
    }
}