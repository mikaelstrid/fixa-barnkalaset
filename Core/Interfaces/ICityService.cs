using System;
using System.Threading.Tasks;
using Pixel.FixaBarnkalaset.Core.Commands;

namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface ICityService
    {
        Task<Guid> When(CreateCity cmd);
    }
}