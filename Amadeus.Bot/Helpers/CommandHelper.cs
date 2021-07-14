using System.Linq;
using System.Threading.Tasks;
using Amadeus.Bot.Models;
using Amadeus.Db.Helper;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;

namespace Amadeus.Bot.Helpers
{
    public static class CommandHelper
    {
        public static async Task<int> ResolvePrefixAsync(DiscordMessage msg, AmadeusConfig cfg)
        {
            if (new[] {MessageType.Default, MessageType.Reply}.All(x => x != msg.MessageType))
                // return -1 if message is not regular message or reply
                return -1;

            if (msg.Author.IsBot)
                // return -1 if msg author is bot
                return -1;

            // get guild prefix and return offset if message begins with it
            var p = await ConfigHelper.GetString("CommandPrefix", msg.Channel?.Guild?.Id);
            return msg.GetStringPrefixLength(p);
        }
    }
}