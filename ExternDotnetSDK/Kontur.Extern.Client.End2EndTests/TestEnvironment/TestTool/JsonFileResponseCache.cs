using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kontur.Extern.Client.End2EndTests.TestEnvironment.TestTool
{
    internal class JsonFileResponseCache : IResponseCache
    {
        private const string FileName = "extern.api.test.tools.response.json";
        
        public static JsonFileResponseCache InTempFolder() => new(Path.GetTempPath());

        public static JsonFileResponseCache InCurrentFolder() => new(Directory.GetCurrentDirectory());
        
        private readonly string path;

        private JsonFileResponseCache(string path) => this.path = path;

        public async Task<string?> TryGetAsync(string key)
        {
            var filePath = GetFilePath();
            if (!File.Exists(filePath))
                return null;

            try
            {
                await using var jsonFile = File.OpenRead(filePath);
                Dictionary<string,string>? dictionary = null;
                if (jsonFile.Length > 0)
                {
                    dictionary = await JsonSerializer.DeserializeAsync<Dictionary<string, string>>(jsonFile).ConfigureAwait(false);
                }
                if (dictionary == null || !dictionary.TryGetValue(key, out var cashedResponse))
                    return null;

                if (string.IsNullOrWhiteSpace(cashedResponse))
                    return null;

                return cashedResponse;
            }
            catch (IOException)
            {
                return null;
            }
        }

        public async Task SetValueAsync(string key, string value)
        {
            await using var jsonFile = File.OpenWrite(GetFilePath());
            Dictionary<string,string>? dictionary = null;
            if (jsonFile.Length > 0)
            {
                dictionary = await JsonSerializer.DeserializeAsync<Dictionary<string, string>>(jsonFile).ConfigureAwait(false);
            }

            dictionary ??= new Dictionary<string, string>();
            dictionary[key] = value;

            jsonFile.Position = 0;
            await JsonSerializer.SerializeAsync(jsonFile, dictionary).ConfigureAwait(false);
        }

        private string GetFilePath() => Path.Combine(path, FileName);
    }
}