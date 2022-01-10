using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using static ChampionMasteryGg.TimeUtility;
using static System.Net.WebUtility;

namespace ChampionMasteryGg
{
    internal static class Parser
    {
        public static async Task<ChampionMasteryGgChampionMastery[]> ParseSummonerMasteries(string html)
        {
            return await Task.Run(() =>
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                List<ChampionMasteryGgChampionMastery> masteries = new();

                foreach (var row in doc.DocumentNode.SelectNodes("//tbody/tr"))
                {
                    var cells = row.SelectNodes(".//td");

                    masteries.Add(new ChampionMasteryGgChampionMastery(
                            HtmlDecode(cells[0].ChildNodes[1].InnerText),
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

    }
}
