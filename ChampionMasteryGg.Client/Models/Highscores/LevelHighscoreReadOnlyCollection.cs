using HtmlAgilityPack;
using System.Collections.ObjectModel;

namespace ChampionMasteryGg
{
    public class LevelHighscoreReadOnlyCollection : ChampionMasteryGgReadOnlyCollection<LevelHighscore>
    {
        internal LevelHighscoreReadOnlyCollection(IList<LevelHighscore> list) : base(list)
        {
        }

        internal static LevelHighscoreReadOnlyCollection FromChampionInfoNode(HtmlNode championInfoNode) => Parser.ParseLevelHighscoreChampionInfoNode(championInfoNode);
    }
}