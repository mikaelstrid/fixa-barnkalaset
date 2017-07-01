namespace Pixel.FixaBarnkalaset.Domain.Commands
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ChangeCityName : CommandBase
    {
        public string NewName { get; }

        public ChangeCityName(string newName)
        {
            NewName = newName;
        }
    }
}