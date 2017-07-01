using System;

namespace Pixel.FixaBarnkalaset.Domain.Commands
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ChangeCitySlug : CommandBase
    {
        public Guid CityId { get; }
        public string NewSlug { get; }

        public ChangeCitySlug(Guid cityId, string newSlug)
        {
            CityId = cityId;
            NewSlug = newSlug;
        }
    }
}