using System;
using System.Threading.Tasks;
using Pixel.FixaBarnkalaset.Domain.Commands;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface ICityService
    {
        Task<Guid> When(CreateCity cmd);
        Task When(ChangeCityName cmd);
        Task When(ChangeCitySlug cmd);
        Task When(ChangeCityPosition changeCityPosition);
    }
}