using HtmlAgilityPack;
using System.Collections;
using static ChampionMasteryGg.Parser;

namespace ChampionMasteryGg
{
    public class ChampionHighscoreReadOnlyDictionary : ChampionMasteryGgReadOnlyDictionary<Champion, PointHighscoreReadOnlyCollection>
    {
        internal ChampionHighscoreReadOnlyDictionary(Dictionary<Champion, PointHighscoreReadOnlyCollection> championHighscores) : base(championHighscores)
        {
        }

        internal ChampionHighscoreReadOnlyDictionary(Dictionary<Champion, PointsHighscore[]> championHighscores) : this(championHighscores.ToDictionary(kvp => kvp.Key, kvp => new PointHighscoreReadOnlyCollection(kvp.Value)))
        {
        }

        internal static ChampionHighscoreReadOnlyDictionary FromHighscoresListNodes(IEnumerable<HtmlNode> divs) => Parser.ParseChampionHighscoreListNodes(divs);
    }
}