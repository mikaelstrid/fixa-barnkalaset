using System;
using Pixel.FixaBarnkalaset.Core.Interfaces;

namespace Pixel.FixaBarnkalaset.Core.Business
{
    public class PartyIdGenerator : IPartyIdGenerator
    {
        private const string AllowedCharacters = "ABCDEFGHJKMNPQRETUVWXYZ2346789";
        private const int Length = 4;

        private readonly Random _random;

        public PartyIdGenerator()
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

        private string GetRandomCharacter()
        {
            return AllowedCharacters[_random.Next(0, AllowedCharacters.Length - 1)].ToString();
        }
    }
}
