using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace Amadeus.Bot.Modules
{
    public class ModerationModule : BaseCommandModule
    {
        [Command("greet")]
        public async Task GreetCommand(CommandContext ctx)
        {
            await ctx.RespondAsync("Greetings! Thank you for executing me!");
        }

        [Command("verify")]
        [Aliases("v")]
        [RequireGuild]
        [RequireBotPermissions(Permissions.ManageRoles)]
        public async Task VerifyCommand(CommandContext ctx, DiscordMember member)
        {
            await Commands.ModerationModule.VerifyCommand.Run(ctx, member);
        }
    }
}