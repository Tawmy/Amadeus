using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Amadeus.Bot.Modules
{
    public class XivModule : BaseCommandModule
    {
        [Command("ffchar")]
        public async Task CharacterProfileCommand(CommandContext ctx, string firstName, string lastName)
        {
            await Commands.XivModule.CharacterProfileCommand.Run(ctx, firstName, lastName, null);
        }

        [Command("ffchar")]
        public async Task CharacterProfileCommand(CommandContext ctx, string firstName, string lastName, string server)
        {
            await Commands.XivModule.CharacterProfileCommand.Run(ctx, firstName, lastName, server);
        }

        [Command("ffchar")]
        public async Task CharacterProfileCommand(CommandContext ctx, string id)
        {
            await Commands.XivModule.CharacterProfileCommand.Run(ctx, id);
        }
    }
}