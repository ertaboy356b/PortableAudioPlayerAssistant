using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PortableAudioPlayerAssistant
{
    public static class JSONExtension
    {
        static JsonSerializerSettings _settings = new JsonSerializerSettings()
        {

        };

        public static T Deserialize<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _settings);
        }

        public static string Serialize<T>(this T item)
        {
            return JsonConvert.SerializeObject(item, Formatting.Indented, _settings);
        }
    }
}
