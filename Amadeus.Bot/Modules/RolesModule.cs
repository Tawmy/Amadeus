using Amadeus.Bot.Checks;
using Amadeus.Bot.Commands.RolesModule;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace Amadeus.Bot.Modules;

public class RolesModule : ApplicationCommandModule
{
    [SlashCommand("postMenu", "Pick a self-assign menu to post in given channel.")]
    [ModeratorSlash]
    [SlashRequireBotPermissions(Permissions.SendMessages)]
    public async Task SlashPostRolesMenu(InteractionContext ctx,
        [Option("Channel", "Channel to send the message in")]
        DiscordChannel? channel = null)
    {
        await PostRolesMenuCommand.RunSlash(ctx, channel);
    }
}