﻿using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Fonctions utile
/// <summary>
public static class ExtList
{
    #region core script

    /// <summary>
    /// Shuffle the list in place using the Fisher-Yates method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    public static void Shuffle<T>(this IList<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    /// <summary>
    /// Return a random item from the list.
    /// Sampling with replacement.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T RandomItem<T>(this IList<T> list)
    {
        if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot select a random item from an empty list");
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    /// <summary>
    /// Removes a random item from the list, returning that item.
    /// Sampling without replacement.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static T RemoveRandom<T>(this IList<T> list)
    {
        if (list.Count == 0) throw new System.IndexOutOfRangeException("Cannot remove a random item from an empty list");
        int index = UnityEngine.Random.Range(0, list.Count);
        T item = list[index];
        list.RemoveAt(index);
        return item;
    }

    /// <summary>
    /// l'item est dans l'array ?
    /// </summary>
    public static bool IsInArray<T>(T[] array, T item)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (EqualityComparer<T>.Default.Equals(array[i], item))
            {
                return (true);
            }
        }
        return (false);
    }

    /// <summary>
    /// l'item est dans l'array ?
    /// </summary>
    public static void ClearArray<T>(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = default(T);
        }
    }

    /// <summary>
    /// Returns true if the array is null or empty
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this T[] data)
    {
        return ((data == null) || (data.Length == 0));
    }

    /// <summary>
    /// Returns true if the list is null or empty
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this List<T> data)
    {
        return ((data == null) || (data.Count == 0));
    }

    /// <summary>
    /// Returns true if the dictionary is null or empty
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T1, T2>(this Dictionary<T1, T2> data)
    {
        return ((data == null) || (data.Count == 0));
    }

    /// <summary>
    /// Removes items from a collection based on the condition you provide. This is useful if a query gives 
    /// you some duplicates that you can't seem to get rid of. Some Linq2Sql queries are an example of this. 
    /// Use this method afterward to strip things you know are in the list multiple times
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="Predicate"></param>
    /// <remarks>http://extensionmethod.net/csharp/icollection-t/removeduplicates</remarks>
    /// <returns></returns>
    public static IEnumerable<T> RemoveDuplicates<T>(this ICollection<T> list, Func<T, int> Predicate)
    {
        var dict = new Dictionary<int, T>();

        foreach (var item in list)
        {
            if (!dict.ContainsKey(Predicate(item)))
            {
                dict.Add(Predicate(item), item);
            }
        }

        return dict.Values.AsEnumerable();
    }

    /// <summary>
    /// deques an item, or returns null
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="q"></param>
    /// <returns></returns>
    public static T DequeueOrNull<T>(this Queue<T> q)
    {
        try
        {
            return (q.Count > 0) ? q.Dequeue() : default(T);
        }

        catch (Exception)
        {
            return default(T);
        }
    }
    #endregion
}
