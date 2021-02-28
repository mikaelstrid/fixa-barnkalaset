using System.Collections.Generic;

namespace Pixel.FixaBarnkalaset.Infrastructure.Identity
{
    public static class Roles
    {
        private static readonly string[] roles = { "Admin" };

        public static string Admin => roles[0];

        public static IEnumerable<string> All => roles;
    }
}