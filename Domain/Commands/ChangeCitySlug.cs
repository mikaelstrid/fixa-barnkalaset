namespace Pixel.FixaBarnkalaset.Domain.Commands
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ChangeCitySlug : CommandBase
    {
        public string NewSlug { get; }

        public ChangeCitySlug(string newSlug)
        {
            NewSlug = newSlug;
        }
    }
}