using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ChampionMasteryGg
{
    [DebuggerDisplay("Champions = {Count}")]
    public class ChampionHighscoresDictionary : ChampionMasteryGgDictionary<Champion, HighscoresCollection<PointsHighscore>>
    {
        internal ChampionHighscoresDictionary(IDictionary<Champion, HighscoresCollection<PointsHighscore>> dictionary) : base(dictionary)
        {
        }
    }
}