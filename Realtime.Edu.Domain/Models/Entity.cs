using System;
using LiteDB;
using Realtime.Edu.Core.Interfaces;

namespace Realtime.Edu.Core.Models
{
    public abstract class Entity : IEntity
    {
        protected Entity(Guid key, string name = "")
        {
            Key = key;
            Name = name;
        }

        [BsonId]
        public Guid Key { get; }
        
        public string Name { get; set; }
    }
}