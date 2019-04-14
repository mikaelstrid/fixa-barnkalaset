using Pixel.FixaBarnkalaset.Core.Interfaces;

namespace Pixel.FixaBarnkalaset.Core.Business
{
    public class InvitationIdGenerator : IdGeneratorBase, IInvitationIdGenerator
    {
        public override int Length => 2;
    }
}
