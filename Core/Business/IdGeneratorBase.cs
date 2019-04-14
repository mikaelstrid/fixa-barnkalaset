using System;
using Pixel.FixaBarnkalaset.Core.Interfaces;

namespace Pixel.FixaBarnkalaset.Core.Business
{
    public abstract class IdGeneratorBase : IIdGenerator
    {
        private const string AllowedCharacters = "ABCDEFGHJKMNPQRETUVWXYZ2346789";

        private readonly Random _random;

        public abstract int Length { get; }

        protected IdGeneratorBase()
        {
            _random = new Random(unchecked((int)DateTime.Now.Ticks));
        }

        public string Next()
        {
            var id = "";
            while (id.Length < Length)
            {
                var c = GetRandomCharacter();
                if (!id.Contains(c))
                    id += c;
            }
            return id;
        }

        public string Concatenate(string first, string second)
        {
            return $"{first}-{second}";
        }

        public (string first, string second) Split(string id)
        {
            var parts = id.Split('-');
            return (parts[0], parts[1]);
        }

        private string GetRandomCharacter()
        {
            return AllowedCharacters[_random.Next(0, AllowedCharacters.Length - 1)].ToString();
        }
    }
}