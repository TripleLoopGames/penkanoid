using System;
using System.Collections.Generic;

public static class IEnumerableExtensions
{
    public static IEnumerable<T2> map<T1,T2>(this IEnumerable<T1> items, Func<T1, int, T2> action)
    {
        int index = 0;
        return items.Select((item) => {
            var value = action(item, index);
            index++;
            return value;
        });
    }

    public static IEnumerable<T2> map<T1, T2>(this IEnumerable<T1> items, Func<T1, T2> action)
    {
        return items.Select((item) => action(item));       
    }
}