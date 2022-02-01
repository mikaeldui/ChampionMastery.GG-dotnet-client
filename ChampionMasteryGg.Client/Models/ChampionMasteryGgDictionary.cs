using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace ChampionMasteryGg
{
    public interface IChampionMasterGgDictionary<TKey, TValue> : IChampionMasteryGgObject, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue>, ICollection, IDictionary
        where TKey : IChampionMasteryGgObject
        where TValue : IChampionMasteryGgObject
    {

    }

    [JsonIReadOnlyDictionary, DebuggerDisplay("Count = {Count}")]
    public abstract class ChampionMasteryGgDictionary<TKey, TValue> : ReadOnlyDictionary<TKey, TValue>, IChampionMasterGgDictionary<TKey, TValue>
        where TKey : IChampionMasteryGgObject
        where TValue : IChampionMasteryGgObject
    {
        internal ChampionMasteryGgDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary)
        {
        }
    }
}
