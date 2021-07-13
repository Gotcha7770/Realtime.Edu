using System;
using System.Reactive.Linq;

namespace Realtime.Edu.Core.Models
{
    public class Project : Entity
    {
        public Project(Guid key, string name = "")
            : base(key, name)
        {
        }
    }
}