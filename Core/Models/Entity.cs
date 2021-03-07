namespace Pixel.FixaBarnkalaset.Core.Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public bool IsRemoved { get; set; }
    }
}