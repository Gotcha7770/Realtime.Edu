using System;

namespace Realtime.Edu.Core.Models
{
    public class Log : Entity
    {
        public Log(Guid key, string name = "")
            : base(key, name)
        {
        }
    }
}