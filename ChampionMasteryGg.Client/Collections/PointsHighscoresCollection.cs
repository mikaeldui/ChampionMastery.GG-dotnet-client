using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
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