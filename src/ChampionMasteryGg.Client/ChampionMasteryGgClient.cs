using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChampionMasteryGg
{
    public class ChampionMasteryGgClient : IDisposable
    {
        private HttpClient _httpClient;

        public ChampionMasteryGgClient()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "ChampionMastery.GG-uwp/dev (github.com/mikaeldui/ChampionMastery.GG-uwp)");
        }

        public async Task<ChampionMasteryGgChampionMastery[]> GetChampionMasteryAsync(string username, string region)
        {
            var result = await _httpClient.GetAsync($"https://championmastery.gg/summoner?region={region}&summoner={Uri.EscapeDataString(username)}");

            return await Task.Run(async () =>
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(await result.Content.ReadAsStringAsync());

                List<ChampionMasteryGgChampionMastery> masteries = new List<ChampionMasteryGgChampionMastery>();

                foreach (var row in doc.DocumentNode.SelectNodes("//tbody/tr"))
                {
                    var cells = row.SelectNodes(".//td");

                    masteries.Add(new ChampionMasteryGgChampionMastery(
                            WebUtility.HtmlDecode(cells[0].ChildNodes[1].InnerText),
                            int.Parse(cells[1].InnerText),
                            int.Parse(cells[2].InnerText),
                            cells[3].Attributes["data-value"].Value == "1",
                            JavaTimeStampToDateTime(double.Parse(cells[4].Attributes["data-value"].Value)),
                            cells[5].ToChampionMasteryProgress()
                        ));
                }

                return masteries.ToArray();
            });
        }

        public void Dispose() => _httpClient.Dispose();

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        public static DateTime JavaTimeStampToDateTime(double javaTimeStamp)
        {
            // Java timestamp is milliseconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(javaTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}