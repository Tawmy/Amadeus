using Amadeus.Bot.Commands.RolesModule;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace Amadeus.Bot.Modules;

public class RolesModule: ApplicationCommandModule
{
    [SlashCommand("roles", "List self-assignable roles")]
    [SlashRequireBotPermissions(Permissions.ManageRoles)]
    public async Task SlashRoles(InteractionContext ctx)
    {
        await new RolesCommand(ctx).RunSlash();
    }
}