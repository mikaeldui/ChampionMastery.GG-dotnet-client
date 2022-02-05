using System.Diagnostics;
using System.Text.Json.Serialization;

namespace ChampionMasteryGg
{
    [JsonReadOnlyCollection, DebuggerDisplay("Highscores = {Count}")]
    public class LevelHighscoresCollection : HighscoresCollection<LevelHighscore>
    {
        public LevelHighscoresCollection(IList<LevelHighscore> list) : base(list)
        {
        }
    }
}