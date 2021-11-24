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
        await Commands.ModerationModule.VerifyCommand.Run(ctx);
    }
}