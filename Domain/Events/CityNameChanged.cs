using System;

namespace Pixel.FixaBarnkalaset.Domain.Events
{
    public class CityNameChanged : EventBase
    {
        public string NewName { get; }
        public string OldName { get; }

        public CityNameChanged(Guid id, string newName, string oldName)
        {
            Id = id;
            NewName = newName;
            OldName = oldName;
        }
    }
}
