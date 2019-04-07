using System;
using System.Collections.Generic;
using System.Text;

namespace Pixel.FixaBarnkalaset.Core.Business
{
    public static class PartyExtensions
    {
        public static string RenderRsvpContactInformation(this Party party)
        {
            if (!string.IsNullOrWhiteSpace(party.RsvpPhoneNumber) && !string.IsNullOrWhiteSpace(party.RsvpEmail))
                return $"{party.RsvpPhoneNumber} eller {party.RsvpEmail}";
            if (!string.IsNullOrWhiteSpace(party.RsvpPhoneNumber))
                return party.RsvpPhoneNumber;
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (!string.IsNullOrWhiteSpace(party.RsvpEmail))
                return party.RsvpEmail;
            return "";
        }
    }
}
