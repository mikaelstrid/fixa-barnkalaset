namespace Pixel.FixaBarnkalaset.Core
{
    public class Guest : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string PostalCity { get; set; }
    }
}
