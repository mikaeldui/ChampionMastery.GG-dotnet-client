using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionMasteryGg.Parsers
{
    internal static class LevelHighscoresParser
    {
        public static async Task<LevelHighscoresCollection> ParseAsync(string html) => await Task.Run(() =>
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            List<LevelHighscore> masteries = new();

            foreach (var row in doc.DocumentNode.SelectNodes("//tbody/tr"))
            {
                var cells = row.SelectNodes(".//td");

                masteries.Add(new(Summoner.FromLinkNode(cells[1].ChildNode("a")), cells[2].Attributes["data-format-number"].Value.ToInt()));
            }

            return new LevelHighscoresCollection(masteries.ToArray());
        });
    }
}
