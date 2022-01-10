using HtmlAgilityPack;
using System.Collections.ObjectModel;

namespace ChampionMasteryGg
{
    public class PointHighscoreReadOnlyCollection : ChampionMasteryGgReadOnlyCollection<PointsHighscore>
    {
        internal PointHighscoreReadOnlyCollection(IList<PointsHighscore> list) : base(list)
        {
        }

        internal static PointHighscoreReadOnlyCollection FromChampionInfoNode(HtmlNode championInfoNode) => Parser.ParsePointHighscoreChampionInfoNode(championInfoNode);
    }
}