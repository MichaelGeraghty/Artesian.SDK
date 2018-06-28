using System;
using System.Collections.Generic;
using System.Linq;

namespace Artesian.SDK.Dependencies
{
    public static class MinMaxExtensions
    {
        public static T MinWith<T>(this T first, params T[] args) where T : IComparable<T>
        {
            IEnumerable<T> objs = new T[1] { first };
            if (args != null)
                objs = objs.Concat(args);
            return objs.Min();
        }

        public static T MaxWith<T>(this T first, params T[] args) where T : IComparable<T>
        {
            IEnumerable<T> objs = new T[1] { first };
            if (args != null)
                objs = objs.Concat(args);
            return objs.Max();
        }
    }
}
