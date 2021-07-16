using System;
using LiteDB;
using Newtonsoft.Json;
using NUnit.Framework;
using Realtime.Edu.Core.Models;
using JsonSerializer = LiteDB.JsonSerializer;

namespace Realtime.Edu.Tests
{
    [TestFixture]
    public class BsonToJsonTests
    {
        [Test]
        public void BsonToJsonTest()
        {            
            var property = new Property {Value = "Test"};
            var settings = new Settings(Guid.NewGuid());
            settings.Add(Guid.NewGuid(), property);
            
            var document = BsonMapper.Global.ToDocument(settings);
            var json = JsonSerializer.Serialize(document);

            var restored = JsonConvert.DeserializeObject<Settings>(json);
        }
    }
}