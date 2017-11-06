namespace Pixel.FixaBarnkalaset.Core.Interfaces
{
    public interface IIdGenerator
    {
        string Next();
        string Concatenate(string first, string second);
        (string first, string second) Split(string id);
    }
}