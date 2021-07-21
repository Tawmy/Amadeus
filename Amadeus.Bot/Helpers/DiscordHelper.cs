using System.Collections.Generic;

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