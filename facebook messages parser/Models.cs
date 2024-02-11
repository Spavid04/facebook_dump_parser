#nullable disable

using Newtonsoft.Json;
using System.Text;

namespace facebook_messages_parser
{
    public class MessageChunk
    {
        [JsonIgnore]
        public string Path;

        [JsonProperty("participants")]
        public List<Participant> Participants;
        [JsonProperty("messages")]
        public List<Message> Messages;
        [JsonProperty("title")]
        public string Title;
        [JsonProperty("is_still_participant")]
        public bool IsParticipant;
        [JsonProperty("thread_type")]
        public string ThreadType;
        [JsonProperty("thread_path")]
        public string ThreadPath;

        [JsonProperty("image")]
        public Photo Thumbnail;
        [JsonProperty("magic_words")]
        public List<bool> MagicWords;

        [JsonProperty("joinable_mode")]
        public object JoinableMode; //todo

        [JsonExtensionData]
        private IDictionary<string, object> _additionalData;

        public void Validate()
        {
            if (this._additionalData != null && this._additionalData.Count > 0)
            {
                throw new FormatException();
            }

            foreach (var participant in this.Participants)
            {
                participant.Validate();
            }
            foreach (var message in this.Messages)
            {
                message.Validate();
            }
        }
    }

    public class Participant
    {
        [JsonProperty("name")]
        public string Name;

        [JsonExtensionData]
        private IDictionary<string, object> _additionalData;

        public void Validate()
        {
            if (this._additionalData != null && this._additionalData.Count > 0)
            {
                throw new FormatException();
            }
        }
    }
    public class Message
    {
        [JsonProperty("sender_name")]
        public string Sender;
        [JsonProperty("timestamp_ms")]
        public long Timestamp;

        [JsonIgnore]
        private DateTime? _timestamp_dt = null;
        [JsonIgnore]
        public DateTime Timestamp_dt => this._timestamp_dt ??= Utils.UnixEpochSecondsConverter.GetDateTime(this.Timestamp / 1000);

        [JsonProperty("content")]
        public string Content;
        [JsonProperty("audio_files")]
        public List<Audio> Audios;
        [JsonProperty("photos")]
        public List<Photo> Photos;
        [JsonProperty("videos")]
        public List<Video> Videos;
        [JsonProperty("gifs")]
        public List<Gif> Gifs;
        [JsonProperty("sticker")]
        public Sticker StickerData;
        [JsonProperty("files")]
        public List<FbFile> Files;

        [JsonProperty("share")]
        public Share ShareData;
        [JsonProperty("users")]
        public List<User> Users;

        [JsonProperty("call_duration")]
        public int? CallDuration;
        [JsonProperty("missed")]
        public bool? CallMissed;

        [JsonProperty("reactions")]
        public List<Reaction> Reactions;

        [JsonProperty("type")]
        public string MessageType;
        [JsonProperty("is_unsent")]
        public bool IsUnsent;

        [JsonProperty("IP")]
        public string IP;

        [JsonProperty("sender_id_INTERNAL")]
        public int? SenderIdInternal;
        [JsonProperty("is_taken_down")]
        public bool IsTakenDown;

        [JsonProperty("is_geoblocked_for_viewer")]
        public bool? IsGeoblockedForViewer;

        public class BumpedMetadata
        {
            [JsonProperty("bumped_message")]
            public string BumpedMessage;
            [JsonProperty("is_bumped")]
            public bool IsBumped;

            [JsonExtensionData]
            private IDictionary<string, object> _additionalData;

            public void Validate()
            {
                if (this._additionalData?.Count > 0)
                {
                    throw new FormatException();
                }
            }
        }

        [JsonProperty("bumped_message_metadata")]
        public BumpedMetadata BumpedMessageMetadata;

        [JsonExtensionData]
        private IDictionary<string, object> _additionalData;

        public void Validate()
        {
            if (this._additionalData != null && this._additionalData.Count > 0)
            {
                throw new FormatException();
            }

            if (this.Audios != null)
            {
                foreach (var audio in this.Audios)
                {
                    audio.Validate();
                }
            }
            if (this.Photos != null)
            {
                foreach (var photo in this.Photos)
                {
                    photo.Validate();
                }
            }
            if (this.Videos != null)
            {
                foreach (var video in this.Videos)
                {
                    video.Validate();
                }
            }
            if (this.Gifs != null)
            {
                foreach (var gif in this.Gifs)
                {
                    gif.Validate();
                }
            }
            if (this.StickerData != null)
            {
                this.StickerData.Validate();
            }
            if (this.Files != null)
            {
                foreach (var file in this.Files)
                {
                    file.Validate();
                }
            }
            if (this.ShareData != null)
            {
                this.ShareData.Validate();
            }
            if (this.Users != null)
            {
                foreach (var user in this.Users)
                {
                    user.Validate();
                }
            }
            if (this.Reactions != null)
            {
                foreach (var reaction in this.Reactions)
                {
                    reaction.Validate();
                }
            }
            this.BumpedMessageMetadata?.Validate();
        }
    }

    public class Audio : FileCommon
    {
        [JsonProperty("creation_timestamp")]
        public long Timestamp;

        [JsonIgnore]
        private DateTime? _timestamp_dt = null;
        [JsonIgnore]
        public DateTime Timestamp_dt => this._timestamp_dt ??= Utils.UnixEpochSecondsConverter.GetDateTime(this.Timestamp / 1000);

        [JsonExtensionData]
        private IDictionary<string, object> _additionalData;

        public void Validate()
        {
            if (this._additionalData != null && this._additionalData.Count > 0)
            {
                throw new FormatException();
            }
        }
    }

    public class Photo : FileCommon
    {
        [JsonProperty("creation_timestamp")]
        public long Timestamp;

        [JsonIgnore]
        private DateTime? _timestamp_dt = null;
        [JsonIgnore]
        public DateTime Timestamp_dt => this._timestamp_dt ??= Utils.UnixEpochSecondsConverter.GetDateTime(this.Timestamp / 1000);

        [JsonExtensionData]
        private IDictionary<string, object> _additionalData;

        public void Validate()
        {
            if (this._additionalData != null && this._additionalData.Count > 0)
            {
                throw new FormatException();
            }
        }
    }

    public class Video : FileCommon
    {
        public class Thumbnail : FileCommon
        {
            [JsonExtensionData]
            private IDictionary<string, object> _additionalData;

            public void Validate()
            {
                if (this._additionalData != null && this._additionalData.Count > 0)
                {
                    throw new FormatException();
                }
            }
        }

        [JsonProperty("creation_timestamp")]
        public long Timestamp;

        [JsonIgnore]
        private DateTime? _timestamp_dt = null;
        [JsonIgnore]
        public DateTime Timestamp_dt => this._timestamp_dt ??= Utils.UnixEpochSecondsConverter.GetDateTime(this.Timestamp / 1000);

        [JsonProperty("Thumbnail")]
        public Thumbnail? ThumbnailImage;

        [JsonExtensionData]
        private IDictionary<string, object> _additionalData;

        public void Validate()
        {
            if (this._additionalData != null && this._additionalData.Count > 0)
            {
                throw new FormatException();
            }

            if (this.ThumbnailImage != null)
            {
                this.ThumbnailImage.Validate();
            }
        }
    }

    public class Gif : FileCommon
    {
        [JsonExtensionData]
        private IDictionary<string, object> _additionalData;

        public void Validate()
        {
            if (this._additionalData != null && this._additionalData.Count > 0)
            {
                throw new FormatException();
            }
        }
    }

    public class Sticker : FileCommon
    {
        [JsonExtensionData]
        private IDictionary<string, object> _additionalData;

        [JsonProperty("ai_stickers")]
        public List<object> AiStickers;

        public void Validate()
        {
            if (this._additionalData != null && this._additionalData.Count > 0)
            {
                throw new FormatException();
            }

            if (this.AiStickers != null && this.AiStickers.Any())
            {
                // todo: what's the structure here?
                throw new FormatException();
            }
        }
    }

    public class FbFile : FileCommon
    {
        [JsonProperty("creation_timestamp")]
        public long Timestamp;

        [JsonIgnore]
        private DateTime? _timestamp_dt = null;
        [JsonIgnore]
        public DateTime Timestamp_dt => this._timestamp_dt ??= Utils.UnixEpochSecondsConverter.GetDateTime(this.Timestamp / 1000);

        [JsonExtensionData]
        private IDictionary<string, object> _additionalData;

        public void Validate()
        {
            if (this._additionalData != null && this._additionalData.Count > 0)
            {
                throw new FormatException();
            }
        }
    }

    public abstract class FileCommon
    {
        [JsonProperty("uri")]
        public string Uri;

        [JsonProperty("media_path_INTERNAL")]
        public string MediaPathInternal;
        [JsonProperty("type_INTERNAL")]
        public string TypeInternal;
    }

    public class Share
    {
        [JsonProperty("link")]
        public string Link;
        [JsonProperty("share_text")]
        public string ShareText;

        [JsonExtensionData]
        private IDictionary<string, object> _additionalData;

        public void Validate()
        {
            if (this._additionalData != null && this._additionalData.Count > 0)
            {
                throw new FormatException();
            }
        }
    }

    public class User
    {
        [JsonProperty("name")]
        public string Name;

        [JsonExtensionData]
        private IDictionary<string, object> _additionalData;

        public void Validate()
        {
            if (this._additionalData != null && this._additionalData.Count > 0)
            {
                throw new FormatException();
            }
        }
    }

    public class Reaction
    {
        [JsonProperty("reaction")]
        public string ReactionStringStupid;
        [JsonProperty("actor")]
        public string Actor;

        [JsonIgnore]
        public string ReactionEmoji
        {
            get
            {
                var bytes = Encoding.Unicode.GetBytes(this.ReactionStringStupid);
                for (int i = 1; i * 2 < bytes.Length; i++)
                {
                    bytes[i] = bytes[i * 2];
                }
                return Encoding.UTF8.GetString(bytes, 0, bytes.Length / 2);
            }
        }

        [JsonExtensionData]
        private IDictionary<string, object> _additionalData;

        public void Validate()
        {
            if (this._additionalData != null && this._additionalData.Count > 0)
            {
                throw new FormatException();
            }
        }
    }
}
