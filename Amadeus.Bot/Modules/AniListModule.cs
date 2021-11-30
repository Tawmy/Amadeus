using Amadeus.Bot.Commands.AniListModule;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace Amadeus.Bot.Modules;

[SlashCommandGroup("AniList", "Get various data from AniList: Anime TODO")]
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
    
    [SlashCommand("manga", "Shows information for the given manga.")]
    [SlashRequireBotPermissions(Permissions.SendMessages | Permissions.EmbedLinks)]
    public async Task SlashManga(InteractionContext ctx,
        [Option("Title", "Title of the manga to search for")]
        string title)
    {
        await MangaCommand.RunSlash(ctx, title);
    }
    
    [SlashCommand("character", "Shows information for the given character.")]
    [SlashRequireBotPermissions(Permissions.SendMessages | Permissions.EmbedLinks)]
    public async Task SlashCharacter(InteractionContext ctx,
        [Option("Name", "Name of the character to search for")]
        string title)
    {
        await CharacterCommand.RunSlash(ctx, title);
    }
}