using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace facebook_messages_parser
{
    public static class Parser
    {
        public static MessageChunk AsThread(IEnumerable<MessageChunk> chunks, bool validate = false)
        {
            var (first, rest) = Utils.HeadAndTail(chunks);
            if (validate) first.Validate();

            first.Path = Path.GetDirectoryName(first.Path);

            foreach (var c in rest)
            {
                if (validate) c.Validate();
                first.Messages.AddRange(c.Messages);
            }
            first.Messages.Sort((x, y) => x.Timestamp.CompareTo(y.Timestamp));

            return first;
        }

        public static IEnumerable<MessageChunk> GetMessageChunks(string directory)
        {
            Regex r = new Regex("^message_\\d+\\.json$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            foreach (var file in Directory.EnumerateFiles(directory))
            {
                if (!r.IsMatch(Path.GetFileName(file)))
                {
                    continue;
                }
                yield return GetMessageChunk(file);
            }
        }

        public static MessageChunk GetMessageChunk(string jsonFile)
        {
            var deserializer = new JsonSerializer();
            deserializer.Converters.Insert(0, new Utils.CustomStringReader());

            using (FileStream fs = new FileStream(jsonFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (StreamReader sr = new StreamReader(fs))
            using (JsonTextReader jr = new JsonTextReader(sr))
            {
                MessageChunk mb = deserializer.Deserialize<MessageChunk>(jr);
                mb.Path = Path.GetFileName(jsonFile);
                return mb;
            }
        }
    }
}
