using System;

namespace OXY.Net.Framework.Utils
{
    public static class DateTimeUtils
    {
        public static bool IsNullOrEmpty(DateTime data)
        {
            if ((data != null) && (data > DateTime.MinValue) && (data < DateTime.MaxValue))
                return false;

            return true;
        }
    }
}
