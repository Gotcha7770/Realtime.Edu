using System;
using DynamicData;

namespace Realtime.Edu.Core.Interfaces
{
    public interface IEntity : IKey<Guid>
    {
        string Name { get; set; }
    }
}