using System.Collections;
using System.Collections.ObjectModel;

namespace ChampionMasteryGg
{

    public interface IHighscoreReadOnlyCollection<T> : IChampionMasteryGgReadOnlyCollection<T>
    where T : IHighscore
    {
    }
}