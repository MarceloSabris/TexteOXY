using Newtonsoft.Json;
using System.IO;

namespace OXY.Net.Framework.ObjectSerializer
{
    public static class JsonSerializer
    {
        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }

        public static T FromJson<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static object FromJson(this string value)
        {
            return JsonConvert.DeserializeObject(value);
        }

        public static T FromJsonStream<T>(this Stream stream)
        {
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            T data;
            using (StreamReader streamReader = new StreamReader(stream))
            {
                data = (T)serializer.Deserialize(streamReader, typeof(T));
            }
            return data;
        }
    }
}
