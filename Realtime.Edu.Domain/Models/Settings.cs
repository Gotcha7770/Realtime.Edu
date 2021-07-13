using System;
using System.Collections.Generic;

namespace Realtime.Edu.Core.Models
{
    public class Settings : Entity
    {
        private readonly List<Property> _entities = new List<Property>();

        public Settings(Guid key) : base(key) { }
        
        public IReadOnlyCollection<Property> Entities => _entities;

        public void Add(Property item)
        {
            _entities.Add(item);
        }
    }
}