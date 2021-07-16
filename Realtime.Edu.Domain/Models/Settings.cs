using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Realtime.Edu.Core.Models
{
    public class Settings : Entity
    {
        //private readonly List<Property> _entities;
        private readonly ConcurrentDictionary<Guid, Property> _entities;

        public Settings(Guid key)
            : base(key)
        {
            // _entities = new List<Property>();
            _entities = new ConcurrentDictionary<Guid, Property>();
        }
        
        public IDictionary<Guid, Property> Entities => _entities;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public void Add(Property item)
        public void Add(Guid id, Property item)
        {
            // _entities.Add(item);
            _entities[id] = item;
        }
    }
}