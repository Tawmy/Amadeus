using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Amadeus.Bot.Modules
{
    public class ModerationModule : BaseCommandModule
    {
        [Command("greet")]
        public async Task GreetCommand(CommandContext ctx)
        {
            await ctx.RespondAsync("Greetings! Thank you for executing me!");
        }
    }
}