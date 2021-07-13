using System;
using System.Collections.Generic;

namespace Realtime.Edu.Core.Models
{
    public class Trajectory : Entity
    {
        private readonly List<Log> _logs = new List<Log>();
        
        public Trajectory(Guid key, string name = "") : base(key, name) { }
        
        public IReadOnlyCollection<Log> Logs => _logs;
    }
}