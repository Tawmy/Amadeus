using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace Amadeus.Bot.Modules
{
    public class ProfileModule : BaseCommandModule
    {
        [Command("profile")]
        public async Task ProfileCommand(CommandContext ctx)
        {
            await Commands.ProfileModule.ProfileCommand.Run(ctx);
        }

        [Command("profile")]
        public async Task ProfileCommand(CommandContext ctx, DiscordMember member)
        {
            await Commands.ProfileModule.ProfileCommand.Run(ctx, member);
        }
        
        [Command("setprofile")]
        public async Task SetProfileCommand(CommandContext ctx)
        {
            await Commands.ProfileModule.SetProfileCommand.Run(ctx);
        }
    }
}