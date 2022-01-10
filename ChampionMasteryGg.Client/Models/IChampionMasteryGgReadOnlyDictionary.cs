using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace ChampionMasteryGg
{
    public interface IChampionMasteryGgReadOnlyDictionary<TKey, TValue> : IChampionMasteryGgObject, IReadOnlyDictionary<TKey, TValue>
        where TKey : IChampionMasteryGgObject
        where TValue : IChampionMasteryGgObject
    {
    }

    [DebuggerStepThrough]
    public class ChampionMasteryGgReadOnlyDictionary<TKey, TValue> : ChampionMasteryGgObject, IChampionMasteryGgReadOnlyDictionary<TKey, TValue>
        where TKey : IChampionMasteryGgObject
        where TValue : IChampionMasteryGgObject
    {
        private readonly Dictionary<TKey, TValue> _dictionary;

        internal ChampionMasteryGgReadOnlyDictionary(Dictionary<TKey, TValue> dictionary)
        {
            _dictionary = dictionary;
        }

        public TValue this[TKey key] => ((IReadOnlyDictionary<TKey, TValue>)_dictionary)[key];

        public IEnumerable<TKey> Keys => ((IReadOnlyDictionary<TKey, TValue>)_dictionary).Keys;

        public IEnumerable<TValue> Values => ((IReadOnlyDictionary<TKey, TValue>)_dictionary).Values;

        public int Count => ((IReadOnlyCollection<KeyValuePair<TKey, TValue>>)_dictionary).Count;

        public bool ContainsKey(TKey key)
        {
            return ((IReadOnlyDictionary<TKey, TValue>)_dictionary).ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<TKey, TValue>>)_dictionary).GetEnumerator();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return ((IReadOnlyDictionary<TKey, TValue>)_dictionary).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_dictionary).GetEnumerator();
        }
    }
}
