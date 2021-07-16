using System;
using Newtonsoft.Json;
using NUnit.Framework;
using Realtime.Edu.Core.Models;

namespace Realtime.Edu.Tests
{
    [TestFixture]
    public class JSONTests
    {
        [Test]
        public void SettingsSerialization()
        {
            var property = new Property {Value = "Test"};
            var settings = new Settings(Guid.NewGuid());
            settings.Add(Guid.NewGuid(), property);
            
            var json = JsonConvert.SerializeObject(settings);
            var restored = JsonConvert.DeserializeObject<Settings>(json);
        }
    }
}