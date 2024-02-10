#nullable disable

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text;

namespace facebook_messages_parser
{
    public static class Utils
    {
        /// <summary>
        /// Most string fields are weirdly serialized.
        /// This fixes them.
        /// </summary>
        public static string CorrectJsonString(this string input)
        {
            if (input == null) return null;
            if (input.Length == 0) return input;

            var chars = input.ToCharArray();
            var bytes = new byte[chars.Length];
            for (int i = 0; i < chars.Length; i++)
            {
                bytes[i] = (byte)chars[i];
            }
            return Encoding.UTF8.GetString(bytes);
        }

        public class CustomStringReader : JsonConverter
        {
            public override bool CanRead => true;

            public override bool CanWrite => false;

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(string);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                return reader.Value.ToString().CorrectJsonString();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }

        public static (T, IEnumerable<T>) HeadAndTail<T>(IEnumerable<T> source)
        {
            var en = source.GetEnumerator();
            en.MoveNext();
            return (en.Current, EnumerateTail<T>(en));
        }

        public static IEnumerable<T> EnumerateTail<T>(IEnumerator<T> en)
        {
            while (en.MoveNext()) yield return en.Current;
        }

        public class UnixEpochSecondsConverter : DateTimeConverterBase
        {
            private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            public static DateTime GetDateTime(long value)
            {
                double seconds = value;
                var dt = DateTime.SpecifyKind(_epoch.AddSeconds(seconds), DateTimeKind.Utc);
                return dt;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteRawValue(((DateTime)value - _epoch).TotalSeconds.ToString());
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.Value == null) { return null; }
                return GetDateTime((long)reader.Value);
            }
        }
    }
}
