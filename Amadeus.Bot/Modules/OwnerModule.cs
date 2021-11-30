using Amadeus.Db.Helper;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace Amadeus.Bot.Modules;

public class OwnerModule : ApplicationCommandModule
{
    [SlashCommand("reloadConfigs", "Reloads all guild configs from database")]
    [SlashRequireOwner]
    public async Task SlashReloadConfigs(InteractionContext ctx)
    {
        await ConfigHelper.LoadGuildConfigs();
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent("Guild configs reloaded"));
    }
}