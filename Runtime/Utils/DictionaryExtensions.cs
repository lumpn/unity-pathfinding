//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Collections.Generic;

public static class DictionaryExtensions
{
    public static V GetOrDefault<K,V>(this IReadOnlyDictionary<K,V> dict, K key, V defaultValue)
    {
        if (dict.TryGetValue(key, out V value))
        {
            return value;
        }
        return defaultValue;
    }

    public static V GetOrAddNew<K, V>(this IDictionary<K, V> dict, K key) where V : new()
    {
        if (!dict.TryGetValue(key, out V value))
        {
            value = new V();
            dict.Add(key, value);
        }
        return value;
    }
}
