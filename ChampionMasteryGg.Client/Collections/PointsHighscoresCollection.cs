using System.Diagnostics;
using System.Text.Json.Serialization;

namespace ChampionMasteryGg
{
    [JsonReadOnlyCollection, DebuggerDisplay("Highscores = {Count}")]
    public class PointsHighscoresCollection : HighscoresCollection<PointsHighscore>
    {
        public PointsHighscoresCollection(IList<PointsHighscore> list) : base(list)
        {
        }
    }
}