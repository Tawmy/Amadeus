using Amadeus.Db.Helper;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Amadeus.Bot.Commands.ConfigurationModule;

public class SetupCommand
{
    private readonly InteractionContext _ctx;
    private readonly DiscordRole _moderatorRole;
    private readonly DiscordChannel _moderatorChannel;
    private readonly DiscordChannel _logChannel;
    private readonly DiscordChannel _archiveChannel;


    public SetupCommand(InteractionContext ctx,
        DiscordRole moderatorRole,
        DiscordChannel moderatorChannel, DiscordChannel logChannel, DiscordChannel archiveChannel)
    {
        _ctx = ctx;
        _moderatorRole = moderatorRole;
        _moderatorChannel = moderatorChannel;
        _logChannel = logChannel;
        _archiveChannel = archiveChannel;
    }

    public async Task RunSlash()
    {
        await _ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);
        
        if (await ConfigHelper.Set(1, _ctx.Guild.Id, _moderatorRole) == false)
        {
            await _ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Failed to set moderator role."));
            return;
        }
        if (await ConfigHelper.Set(2, _ctx.Guild.Id, _moderatorChannel) == false)
        {
            await _ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Failed to set moderator channel."));
            return;
        }
        if (await ConfigHelper.Set(3, _ctx.Guild.Id, _logChannel) == false)
        {
            await _ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Failed to set log channel."));
            return;
        }
        if (await ConfigHelper.Set(4, _ctx.Guild.Id, _archiveChannel) == false)
        {
            await _ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Failed to set archive channel."));
            return;
        }

        await _ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Setup complete!"));
    }
}