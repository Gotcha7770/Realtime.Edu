using System;
using LiteDB;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Realtime.Edu.Core.Interfaces;
using JsonReader = Newtonsoft.Json.JsonReader;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using JsonWriter = Newtonsoft.Json.JsonWriter;

namespace Realtime.Edu.Core.Models
{
    public class BsonIdConverter : JsonConverter<Guid>
    {
        public override void WriteJson(JsonWriter writer, Guid value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override Guid ReadJson(JsonReader reader,
            Type objectType,
            Guid existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            //var value = reader.Read();
            JObject jObject = JObject.Load(reader);
            return jObject["$guid"].ToObject<Guid>();
        }
    }
    
    public abstract class Entity : IEntity
    {
        protected Entity(Guid key, string name = "")
        {
            Key = key;
            Name = name;
        }

        [BsonId]
        [JsonProperty("_id")]
        [JsonConverter(typeof(BsonIdConverter))]
        public Guid Key { get; }
        
        public string Name { get; set; }
    }
}