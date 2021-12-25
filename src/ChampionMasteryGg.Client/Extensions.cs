using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionMasteryGg
{
    internal static class Extensions
    {
        public static IChampionMasteryGgChampionMasteryProgress ToChampionMasteryProgress(this HtmlNode node)
        {
            var title = node.Attributes["title"].Value;

            if (title == "Max level")
                return new ChampionMasteryGgChampionMasteryProgressMastered();
            else if (title.EndsWith("tokens"))
            {
                var dataValue = node.Attributes["data-value"].Value;
                switch (dataValue.Substring(0, 2))
                {
                    case "60":
                        return new ChampionMasteryGgChampionMasteryProgressTokens(int.Parse(dataValue.Substring(2, 1)), 3);
                    case "50":
                        return new ChampionMasteryGgChampionMasteryProgressTokens(int.Parse(dataValue.Substring(2, 1)), 2);
                    default:
                        throw new ArgumentException("Unable to determine progress type.");
                }
            }
            else if (title.Contains("points"))
            {
                var titleSplit = title.Split('/');
                titleSplit[1] = titleSplit[1].Split(' ')[0];
                return new ChampionMasteryGgChampionMasteryProgressPoints(int.Parse(titleSplit[0]), int.Parse(titleSplit[1]));
            }
            else
                throw new ArgumentException("Unable to determine progress type.");
        }
    }
}