using System;
using System.Threading.Tasks;
using Pixel.FixaBarnkalaset.Domain.Commands;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface ICityService
    {
        Task<Guid> When(CreateCity cmd);
        void When(ChangeCityName cmd);
        void When(ChangeCitySlug cmd);
        void When(ChangeCityPosition changeCityPosition);
    }
}