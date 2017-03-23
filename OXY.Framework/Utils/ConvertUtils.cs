using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace OXY.Net.Framework.Utils
{
    public static class ConvertUtils
    {
        public static byte[] String2Bytes(string Text)
        {
            return Encoding.Default.GetBytes(Text);
        }

        public static string Bytes2String(byte[] Text)
        {
            string result = "";
            using (MemoryStream ms = new MemoryStream(Text.Length))
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    foreach (byte c in Text)
                    {
                        if (c != 0)
                        {
                            byte b = (byte)c;
                            bw.Write(b);
                        }
                    }
                }

                result = Encoding.Default.GetString(ms.ToArray());
            }
            return result;
        }

        public static Int32 ToInt32(Object value, Int32 defaultValue)
        {
            try
            {
                return Convert.ToInt32(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static Int16 ToInt16(Object value, Int16 defaultValue)
        {
            try
            {
                return Convert.ToInt16(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static Int64 ToInt64(Object value, Int64 defaultValue)
        {
            try
            {
                return Convert.ToInt64(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static Double ToDouble(Object value, Double defaultValue)
        {
            try
            {
                return Convert.ToDouble(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static DateTime ToDateTime(Object value)
        {
            return ToDateTime(value, DateTime.MinValue);
        }

        public static DateTime ToDateTime(Object value, DateTime defaultValue, string format = "dd/MM/yyyy HH:mm:ss")
        {
            try
            {
                var culture = new CultureInfo("pt-BR", true);
                return DateTime.ParseExact(value.ToString(), format, culture);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static DateTime UnixTimeToDateTime(long unixTimeMillis)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(unixTimeMillis);
        }

        public static String ToString(Object value, String defaultValue)
        {
            try
            {
                return Convert.ToString(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static String ToString(Object value)
        {
            try
            {
                return Convert.ToString(value);
            }
            catch
            {
                return String.Empty;
            }
        }

        public static Boolean ToBoolean(Object value, Boolean defaultValue)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        private static T InternalToEnum<T>(Object value)
        {
            return (T)Enum.ToObject(typeof(T), value);
        }

        public static T ToEnum<T>(Int32 value)
        {
            return InternalToEnum<T>(value);
        }

        public static T ToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static String ListCodigosToString(List<Int32> listCodigos)
        {
            string strListCodigos = string.Empty;

            for (int iCount = 0; iCount <= listCodigos.Count - 1; iCount++)
                strListCodigos += listCodigos[iCount].ToString() + ",";

            if (strListCodigos.Length > 1)
                strListCodigos = strListCodigos.Substring(0, strListCodigos.Length - 1);

            return strListCodigos;
        }

        public static String ListCodigosToString(List<String> listCodigos)
        {
            return ListCodigosToString(listCodigos, false);
        }

        public static String ListToString(String[] ListStr)
        {
            String Result = "";
            foreach (var str in ListStr)
            {
                Result += str + " ";
            }
            return Result;
        }

        public static String ListCodigosToString(List<String> listCodigos, bool quotedStr)
        {
            string strQuote = "";
            if (quotedStr)
                strQuote = "'";

            string strListCodigos = string.Empty;

            for (int iCount = 0; iCount <= listCodigos.Count - 1; iCount++)
                strListCodigos += strQuote + listCodigos[iCount] + strQuote + ",";

            if (strListCodigos.Length > 1)
                strListCodigos = strListCodigos.Substring(0, strListCodigos.Length - 1);

            return strListCodigos;
        }

        public static List<String> StringToListCodigosStr(String stringCSV)
        {
            List<Int32> retorno = new List<Int32>();
            string[] arrayString = stringCSV.Split(',');
            return arrayString.ToList();
        }

        public static List<Int32> StringToListCodigosInt32(String stringCSV)
        {
            List<Int32> retorno = new List<Int32>();

            if (string.IsNullOrEmpty(stringCSV))            
                return retorno;            

            string[] arrayString = stringCSV.Split(',');
            foreach (String s in arrayString)
            {
                Int32 number = ConvertUtils.ToInt32(s, 0);
                retorno.Add(number);
            }
            return retorno;
        }

        public static decimal ToDecimal(object texto)
        {
            decimal valor;
            Decimal.TryParse(texto.ToString(), out valor);
            return valor;
        }

        public static decimal ToDecimal(object texto, int casasDecimais)
        {
            decimal valor;
            Decimal.TryParse(texto.ToString(), out valor);
            return Math.Round(valor, casasDecimais);
        }

        public static decimal ToDecimal_2Casas(object texto)
        {
            return ToDecimal(texto, 2);
        }

        public static bool IsNumeric(String value, bool emptyIsNumeric)
        {
            try
            {
                if ((emptyIsNumeric) && (String.IsNullOrEmpty(value.Trim())))
                    return true;

                Convert.ToDouble(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ToStringBase64(String value)
        {
            return Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(value));
        }
    }
}
