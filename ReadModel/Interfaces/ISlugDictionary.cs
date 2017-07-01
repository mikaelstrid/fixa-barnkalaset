using System;

namespace Pixel.FixaBarnkalaset.ReadModel.Interfaces
{
    public interface ISlugDictionary
    {
        Guid? GetId(string slug);
    }
}
