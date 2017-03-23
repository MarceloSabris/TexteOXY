using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;
using System.Collections.Generic;

using Newtonsoft.Json;
using OXY.Framework.Tipos;

namespace OXY.Net.Framework.Extender
{
    
    public static class ObjectExtender
    {
        public static string GetStringAppSettingsConfig(this string keyName)
        {
            try
            {
                return ConfigurationManager.AppSettings[keyName];
            }
            catch
            {
                return "";
            }
        }

        public static int GetInt32AppSettingsConfig(this string keyName)
        {
            try
            {
                return Convert.ToInt32(GetStringAppSettingsConfig(keyName));
            }
            catch
            {
                return 0;
            }
        }

        public static bool GetBooleanAppSettingsConfig(this string keyName)
        {
            try
            {
                return Boolean.Parse(GetStringAppSettingsConfig(keyName));
            }
            catch
            {
                return false;
            }
        }

        public static XmlDocument ToXmlDocument(this string xml)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                return doc;
            }
            catch
            {
                return default(XmlDocument);
            }
        }

        public static string XmlToString(this XDocument doc)
        {
            if (doc == null)
                throw new ArgumentNullException("Objeto XDocument vazio ou nulo");
            
            var builder = new StringBuilder();
            using (var writer = new StringWriter(builder))
            {
                doc.Save(writer);
            }
            return builder.ToString();
        }

        public static string XmlToString(this XElement element)
        {
            if (element == null)
                throw new ArgumentNullException("Objeto XElement vazio ou nulo");

            var builder = new StringBuilder();
            using (var writer = new StringWriter(builder))
            {
                element.Save(writer);
            }
            return builder.ToString();
        }

   

        public static string FormatDateJson(this DateTime date)
        {
            return FormatDateJson(date, DateFormatOptions.KeepOriginalValue);
        }

        public static string FormatDateJson(this DateTime date, DateFormatOptions formatOptions = DateFormatOptions.KeepOriginalValue)
        {
            if (formatOptions.Equals(DateFormatOptions.NoMilliSecs))
            {
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
            }
            return date.ToString("o");
        }

        public static DateTime FormatStringToDateJson(this string dateStr)
        {
            return FormatStringToDateJson(dateStr, DateFormatOptions.KeepOriginalValue);
        }

        public static DateTime FormatStringToDateJson(this string dateStr, DateFormatOptions formatOptions)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime date = DateTime.ParseExact(dateStr, "o", provider);
            if (formatOptions.Equals(DateFormatOptions.NoMilliSecs))
            {
                date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
            }
            return date;
        }

        public static string ToStringSN(this bool value)
        {
            if (value)
                return "S";
            else
                return "N";
        }

        public static string ConnectionString(this string str)
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[str].ConnectionString;
            }
            catch
            {
                return "";
            }
        }

        public static ConnectionStringSettings ConnectionStringSettings(this string str)
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[str];
            }
            catch
            {
                return null;
            }
        }

        public static Type GetListType<T>(this IList<T> _)
        {
            return typeof(T);
        }

        public static Type GetDictionaryType<TKey, TValue>(this IDictionary<TKey, TValue> _)
        {
            return typeof(TValue);
        }

        [Obsolete("Utilizar a classe JsonSerializer", true)]
        public static String ToJson(this String obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
