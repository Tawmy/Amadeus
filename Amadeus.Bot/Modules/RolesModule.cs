using Amadeus.Bot.Checks;
using Amadeus.Bot.Commands.RolesModule;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace Amadeus.Bot.Modules;

public class RolesModule : ApplicationCommandModule
{
    [SlashCommand("rolesMsg", "Sends message with button for self-assigning roles.")]
    [ModeratorSlash]
    [SlashRequireBotPermissions(Permissions.SendMessages)]
    public async Task SlashRolesMsg(InteractionContext ctx,
        [Option("Title", "Title of the message")]
        string title,
        [Option("Description", "Description of the message")]
        string? description = null,
        [Option("Channel", "Channel to send the message in")]
        DiscordChannel? channel = null)
    {
        await RolesMsgCommand.RunSlash(ctx, title, description, channel);
    }
}