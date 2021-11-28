using Amadeus.Bot.Commands.AniListModule;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace Amadeus.Bot.Modules;

public class AniListModule : ApplicationCommandModule
{
    [SlashCommand("anime", "Shows information for the given anime.")]
    [SlashRequireBotPermissions(Permissions.SendMessages | Permissions.EmbedLinks)]
    public async Task SlashAnime(InteractionContext ctx,
        [Option("Title", "Title of the anime to search for")]
        string title)
    {
        await AnimeCommand.RunSlash(ctx, title);
    }
}