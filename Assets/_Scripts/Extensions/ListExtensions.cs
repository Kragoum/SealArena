using System;
using System.Collections.Generic;

namespace _Scripts.Extensions
{
    public static  class ListExtensions
    {
        public static T Draw<T>(this List<T> list)
        {
            if(list.Count == 0) return default;
            int r = UnityEngine.Random.Range(0, list.Count);
            T t = list[r];
            list.RemoveAt(r);
            return t;
        }
    }
}