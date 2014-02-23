using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PollSystem.Web
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> SkipElementAtIndexes<T>(this IEnumerable<T> collection, params int[] indexes)
        {
            int index = 0;
            foreach (var item in collection)
            {
                if (!indexes.Contains(index))
                {
                    yield return item;
                }

                index++;
            }
        }
    }
}