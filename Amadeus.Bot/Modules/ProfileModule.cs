using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Amadeus.Bot.Modules
{
    public class ProfileModule : BaseCommandModule
    {
        [Command("profile")]
        public async Task ProfileCommand(CommandContext ctx)
        {
            await Commands.ProfileModule.ProfileCommand.Run(ctx);
        }
    }
}