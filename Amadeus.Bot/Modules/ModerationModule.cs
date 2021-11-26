using Amadeus.Bot.Checks;
using Amadeus.Bot.Commands.ModerationModule;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace Amadeus.Bot.Modules;

public class ModerationModule : ApplicationCommandModule
{
    [ContextMenu(ApplicationCommandType.UserContextMenu, "Verify")]
    [ModeratorMenu]
    [SlashRequireBotPermissions(Permissions.ManageRoles)]
    public async Task MenuVerify(ContextMenuContext ctx)
    {
        await VerifyCommand.RunMenu(ctx);
    }

    [SlashCommand("verify", "Verifies a user")]
    [ModeratorSlash]
    [SlashRequireBotPermissions(Permissions.ManageRoles)]
    public async Task SlashVerify(InteractionContext ctx, [Option("User", "User to verify")] DiscordUser user)
    {
        await VerifyCommand.RunSlash(ctx, user);
    }

    [SlashCommand("archive", "Archives messages from the given channel")]
    [ModeratorSlash]
    [SlashRequireBotPermissions(Permissions.ReadMessageHistory)]
    public async Task SlashArchive(InteractionContext ctx,
        [Option("Channel", "Channel to archive")]
        DiscordChannel channel,
        [Option("Messages", "# of messages to archive. Default and maximum is 1000.")]
        long msgs = 1000)
    {
        await new ArchiveCommand(ctx, channel, msgs).RunSlash();
    }
}