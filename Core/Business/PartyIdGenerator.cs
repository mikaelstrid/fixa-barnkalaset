using System;
using Pixel.FixaBarnkalaset.Core.Interfaces;

namespace Pixel.FixaBarnkalaset.Core.Business
{
    public class PartyIdGenerator : IPartyIdGenerator
    {
        private const int NumberOfAttempts = 10000;
        private const string AllowedCharacters = "ABCDEFGHJKMNPQRETUVWXYZ2346789";
        private const int Length = 4;

        private readonly IPartyRepository _partyRepository;
        private readonly Random _random;

        public PartyIdGenerator(IPartyRepository partyRepository)
        {
            _partyRepository = partyRepository;
            _random = new Random(unchecked((int)DateTime.Now.Ticks));
        }

        public string Next()
        {
            var attempt = 0;

            while (attempt < NumberOfAttempts)
            {
                attempt++;

                var id = "";
                while (id.Length < Length)
                {
                    var c = GetRandomCharacter();
                    if (!id.Contains(c))
                        id += c;
                }

                if (_partyRepository.GetById(id) == null)
                    return id;
            }

            throw new Exception($"Could not generate new id in {NumberOfAttempts} attempts");
        }

        private string GetRandomCharacter()
        {
            return AllowedCharacters[_random.Next(0, AllowedCharacters.Length - 1)].ToString();
        }
    }
}
