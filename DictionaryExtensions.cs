using System;
using System.Collections.Generic;
using System.Linq;

namespace RunicMonsterHighlighter
{
    public static class DictionaryExtensions
    {
        public static void RemoveAllByKey<TKey, TValue>(this IDictionary<TKey, TValue> dict, Func<TKey, bool> predicate)
        {
            var keysToBeRemoved = dict.Keys.Where(predicate).ToList();
            foreach (var keyToBeRemoved in keysToBeRemoved)
            {
                dict.Remove(keyToBeRemoved);
            }
        }
    }
}