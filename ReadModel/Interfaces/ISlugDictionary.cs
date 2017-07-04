﻿using System;

namespace Pixel.FixaBarnkalaset.ReadModel.Interfaces
{
    public interface ISlugLookup
    {
        Guid? GetIdBySlug(string slug);
        void AddSlug(string slug, Guid id);
        void RemoveSlug(string slug);
    }
}