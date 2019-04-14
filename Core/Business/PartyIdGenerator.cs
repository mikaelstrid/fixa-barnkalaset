using Pixel.FixaBarnkalaset.Core.Interfaces;

namespace Pixel.FixaBarnkalaset.Core.Business
{
    public class PartyIdGenerator : IdGeneratorBase, IPartyIdGenerator
    {
        public override int Length => 4;
    }
}
