namespace Pixel.FixaBarnkalaset.Core
{
    public class Invitation : Entity<string>
    {
        public string PartyId { get; set; }
        public Party Party { get; set; }

        public int GuestId { get; set; }
        public Guest Guest { get; set; }
    }
}
