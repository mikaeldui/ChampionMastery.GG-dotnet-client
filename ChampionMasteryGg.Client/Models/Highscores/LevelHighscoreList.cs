using System.Collections.ObjectModel;

namespace ChampionMasteryGg
{
    public class LevelHighscoreList : ChampionMasteryGgReadOnlyCollection<LevelHighscore>
    {
        internal LevelHighscoreList(IList<LevelHighscore> list) : base(list)
        {
        }
    }
}