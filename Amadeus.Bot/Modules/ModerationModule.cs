using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace Amadeus.Bot.Modules;

public class ModerationModule : ApplicationCommandModule
{
    [ContextMenu(ApplicationCommandType.UserContextMenu, "Verify")]
    [SlashRequirePermissions(Permissions.ManageRoles)]
    public async Task MenuVerify(ContextMenuContext ctx)
    {
        await Commands.ModerationModule.VerifyCommand.RunMenu(ctx);
    }
    
    [SlashCommand("verify", "Verifies a user")]
    public async Task SlashVerify(InteractionContext ctx, [Option("User", "User to verify")] DiscordUser user)
    {
        await Commands.ModerationModule.VerifyCommand.RunSlash(ctx, user);
    }
}