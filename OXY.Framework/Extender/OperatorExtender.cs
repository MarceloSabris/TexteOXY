using System.Linq;

namespace OXY.Net.Framework.Extender
{
    public static class OperatorExtender
    {
        public static bool In<T>(this T item, params T[] items)
        {
            if (items == null || items.Count() == 0)
                return false;

            return items.Contains(item);
        }

    }
}
