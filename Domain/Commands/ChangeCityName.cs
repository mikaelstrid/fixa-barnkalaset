using System;

namespace Pixel.FixaBarnkalaset.Domain.Commands
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ChangeCityName : CommandBase
    {
        public Guid CityId { get; }
        public string NewName { get; }

        public ChangeCityName(Guid cityId, string newName)
        {
            CityId = cityId;
            NewName = newName;
        }
    }
}