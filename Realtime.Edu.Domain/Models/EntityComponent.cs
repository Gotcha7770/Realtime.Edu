using System;
using System.Collections.Generic;
using DynamicData;
using LiteDB;

namespace Realtime.Edu.Core.Models
{
    public class EntityComponent : IKey<Guid>
    {
        public EntityComponent(string name)
        {
            Name = name;
        }

        [BsonId]
        public Guid Key { get; } = Guid.NewGuid();

        public virtual string Name { get; }

        public List<EntityComponent> Components { get; } = new List<EntityComponent>();
    }

    public class EntityComponent<T> : EntityComponent where T : Entity
    {
        public T Data { get; set; }

        public override string Name => Data.Name;

        public EntityComponent(string name)
            : base(name)
        {
        }
    }
}