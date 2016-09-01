using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

namespace Logentries
{
    public class LogentriesService
    {
        private readonly JArray Logentries;
        
        public LogentriesService(string logentriesJson)
        {
            this.Logentries = JArray.Parse(logentriesJson);
        }

        public ICollection<string> ConvertToLogentries(string tagPattern = null)
        {
            if (!string.IsNullOrWhiteSpace(tagPattern))
            {
                var rx = new Regex(tagPattern, RegexOptions.None);
                return this.Logentries.Where(x => rx.IsMatch(x["m"].ToString()))
                    .Select(x => x["m"].ToString()).ToList();
            }

            return this.Logentries.Select(x =>x["m"].ToString()).ToList();
        }

        public static async Task<LogentriesService> GetLogsAsync(
            LogentriesUrlFactory logentriesUrlFactory,
            DateTime universalDateTimeStart,
            DateTime universalDateTimeEnd,
            string filter = null)
        {
            try
            {
                var startInMilliseconds = (long)(universalDateTimeStart - new DateTime(1970, 1, 1)).TotalMilliseconds;
                var endInMilliseconds = (long)(universalDateTimeEnd - new DateTime(1970, 1, 1)).TotalMilliseconds;

                var uri = GetUri(logentriesUrlFactory.Url, startInMilliseconds, endInMilliseconds, filter);

                var logentriesJson = await GetLogEntriesAsync(uri);
                return new LogentriesService(logentriesJson);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static async Task<string> GetLogEntriesAsync(Uri uri)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                return await client.GetStringAsync(uri);
            }
        }

        private static Uri GetUri(string url, double startInMilliseconds, double endInMilliseconds, string filter = null, int limit = 0)
        {
            string content = $"{url}?format=json&start={startInMilliseconds}";
            content += $"&end={endInMilliseconds}";

            if (!string.IsNullOrEmpty(filter))
            {
                content += $"&filter={filter}";
            }

            if (limit > 0)
            {
                content += $"&limit={limit}";
            }

            return new Uri(content);
        }
    }
}
