using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ChampionMasteryGg
{
    public interface IChampionMasteryGgReadOnlyCollection<T> : IChampionMasteryGgObject, ICollection<T>, IEnumerable<T>, IEnumerable, IList<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, ICollection, IList
        where T : IChampionMasteryGgObject
    {
    }

    [DebuggerStepThrough]
    public class ChampionMasteryGgReadOnlyCollection<T> : ReadOnlyCollection<T>, IChampionMasteryGgReadOnlyCollection<T>
    where T : IChampionMasteryGgObject
    {
        internal ChampionMasteryGgReadOnlyCollection(IList<T> list) : base(list)
        {
        }
    }
}