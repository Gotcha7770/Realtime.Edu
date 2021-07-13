using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Realtime.Edu.Core.Models
{
    public class Well : Entity
    {
        private readonly List<Trajectory> _trajectories = new List<Trajectory>();

        public Well(Guid key, string name = "") : base(key, name) { }

        public IReadOnlyCollection<Trajectory> Trajectories => _trajectories;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(Trajectory item)
        {
            _trajectories.Add(item);
        }
    }
}