using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace ChampionMasteryGg
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IChampionMasterGgCollection<T> : IChampionMasteryGgObject, IEnumerable<T>, IReadOnlyCollection<T>, IReadOnlyList<T>, IEnumerable, ICollection
        where T : IChampionMasteryGgObject
    {

    }

    [EditorBrowsable(EditorBrowsableState.Never), DebuggerDisplay("Count = {Count}")]
    public abstract class ChampionMasteryGgCollection<T> : ReadOnlyCollection<T>, IChampionMasterGgCollection<T>
        where T : IChampionMasteryGgObject
    {
        public ChampionMasteryGgCollection(IList<T> list) : base(list)
        {
        }
    }
}