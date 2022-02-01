using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionMasteryGg.Parsers
{
    internal static class HighscoresParser
    {
        public static async Task<Highscores> ParseAsync(string html) => await Task.Run(() =>
        {
            var doc = new HtmlDocument();

            doc.LoadHtml(html);

            var highscoresNode = doc.DocumentNode.SelectSingleNode("//div[@id='highscores']");

            var highscoresCellNodes = highscoresNode.ChildNodes("div");

            return new Highscores(
                ParsePointHighscoreChampionInfoNode(highscoresCellNodes.First().SelectSingleNode(".//div[@class='champion-info']")),
                ParseLevelHighscoreChampionInfoNode(highscoresCellNodes.Skip(1).First().SelectSingleNode(".//div[@class='champion-info']")),
                ParseChampionHighscoreListNodes(highscoresCellNodes.Skip(2).ToArray()));
        });

        private static PointsHighscore ParsePointsHighscore(HtmlNode div) =>
                new(Summoner.FromLinkNode(div.ChildNodes("a").First()),
                    div.ChildNodes("span").First().Attributes["data-format-number"].Value.ToInt());

        private static LevelHighscore ParseLevelsHighscore(HtmlNode div) =>
                new(Summoner.FromLinkNode(div.ChildNodes("a").First()),
                    div.ChildNodes("span").First().Attributes["data-format-number"].Value.ToInt());

        private static PointsHighscoresCollection ParsePointHighscoreChampionInfoNode(HtmlNode championInfoNode) =>
            new(championInfoNode.ChildNodes("div").Select(ParsePointsHighscore).ToArray());

        private static LevelHighscoresCollection ParseLevelHighscoreChampionInfoNode(HtmlNode championInfoNode) =>
            new(championInfoNode.ChildNodes("div").Select(ParseLevelsHighscore).ToArray());

        private static ChampionHighscoresDictionary ParseChampionHighscoreListNodes(IEnumerable<HtmlNode> divs)
        {
            Dictionary<Champion, HighscoresCollection<PointsHighscore>> championHighscores = new();
            foreach (var cell in divs)
            {
                var championInfo = cell.SelectSingleNode(".//div[@class='champion-info']");
                championHighscores.Add(
                    Champion.FromLinkNode(championInfo.ChildNodes("a").First()),
                    ParsePointHighscoreChampionInfoNode(championInfo));
            }
            return new(championHighscores);
        }
    }
}
