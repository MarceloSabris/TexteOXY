using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OXY.Net.Framework.Extender
{
    public static class IEnumerableExtender
    {
        public static bool IsNullOrZero<T>(this IEnumerable<T> list)
        {
            return (list == null || list.Count() == 0);
        }
    }
}
