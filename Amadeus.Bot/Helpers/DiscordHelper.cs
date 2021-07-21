using System.Collections.Generic;
using System.Linq;

namespace Amadeus.Bot.Helpers
{
    public static class DiscordHelper
    {
        public static IEnumerable<string> ImageFormats()
        {
            return new[] {"jpg", "jpeg", "png", "webp", "gif"};
        }
    }
}