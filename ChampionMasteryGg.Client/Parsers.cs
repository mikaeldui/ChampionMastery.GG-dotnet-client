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
        public static async Task<Mastery[]> ParseSummonerMasteries(string html)
        {
            return await Task.Run(() =>
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                List<Mastery> masteries = new();

                foreach (var row in doc.DocumentNode.SelectNodes("//tbody/tr"))
                {
                    var cells = row.SelectNodes(".//td");

                    masteries.Add(new Mastery(
                            Champion.FromLinkNode(cells[0].ChildNodes[1]),
                            cells[1].InnerText.ToInt(),
                            cells[2].InnerText.ToInt(),
                            cells[3].Attributes["data-value"].Value == "1",
                            JavaTimeStampToDateTime(double.Parse(cells[4].Attributes["data-value"].Value)),
                            cells[5].ToChampionMasteryProgress()
                        ));
                }

                return masteries.ToArray();
            });
        }

        public static async Task<Highscores> ParseHighscores(string html)
        {
            return await Task.Run(() =>
            {
                var doc = new HtmlDocument();

                doc.LoadHtml(html);

                var highscoresNode = doc.DocumentNode.SelectSingleNode("//div[@id='highscores']");

                var highscoresCellNodes = highscoresNode.ChildNodes("div");

                var pointsHighscoreNode = highscoresCellNodes.First();

                var levelHighscoreNode = highscoresCellNodes.Skip(1).First();

                return new Highscores(
                    ParsePointHighscoreChampionInfoNode(pointsHighscoreNode.SelectSingleNode(".//div[@class='champion-info']")),
                    ParseLevelHighscoreChampionInfoNode(levelHighscoreNode.SelectSingleNode(".//div[@class='champion-info']")),
                    ParseChampionHighscoreListNodes(highscoresCellNodes.Skip(2).ToArray()));
            });
        }

        public static PointsHighscore ParsePointsHighscore(HtmlNode div) =>
                new PointsHighscore(
                    Summoner.FromLinkNode(div.ChildNodes("a").First()),
                    div.ChildNodes("span").First().Attributes["data-format-number"].Value.ToInt());

        public static LevelHighscore ParseLevelsHighscore(HtmlNode div) =>
                new LevelHighscore(
                    Summoner.FromLinkNode(div.ChildNodes("a").First()),
                    div.ChildNodes("span").First().Attributes["data-format-number"].Value.ToInt());

        internal static PointHighscoreReadOnlyCollection ParsePointHighscoreChampionInfoNode(HtmlNode championInfoNode) =>
            new PointHighscoreReadOnlyCollection(championInfoNode.ChildNodes("div").Select(ParsePointsHighscore).ToArray());

        internal static LevelHighscoreReadOnlyCollection ParseLevelHighscoreChampionInfoNode(HtmlNode championInfoNode) =>
            new LevelHighscoreReadOnlyCollection(championInfoNode.ChildNodes("div").Select(ParseLevelsHighscore).ToArray());

        internal static ChampionHighscoreReadOnlyDictionary ParseChampionHighscoreListNodes(IEnumerable<HtmlNode> divs)
        {
            Dictionary<Champion, PointHighscoreReadOnlyCollection> championHighscores = new();
            foreach (var cell in divs)
            {
                var championInfo = cell.SelectSingleNode(".//div[@class='champion-info']");
                championHighscores.Add(
                    Champion.FromLinkNode(championInfo.ChildNodes("a").First()),
                    ParsePointHighscoreChampionInfoNode(championInfo));
            }
            return new ChampionHighscoreReadOnlyDictionary(championHighscores);
        }
    }
}
