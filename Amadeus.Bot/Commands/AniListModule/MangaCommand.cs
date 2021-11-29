using Amadeus.Bot.Helper;
using Anilist4Net;
using Anilist4Net.Enums;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Amadeus.Bot.Commands.AniListModule;

public static class MangaCommand
{
    public static async Task RunSlash(InteractionContext ctx, string title)
    {
        await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

        var manga = int.TryParse(title, out var result)
            ? await new Client().GetMediaById(result)
            : await new Client().GetMediaBySearch(title, MediaTypes.MANGA);

        if (manga == null ||
            !new[] {MediaFormats.MANGA, MediaFormats.NOVEL, MediaFormats.ONE_SHOT}.Contains(manga.Format))
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"Manga \"{title}\" not found"));
            return;
        }

        var embed = new DiscordEmbedBuilder();
        embed.AddCommonMediaFieldsTop(manga!);
        embed.AddFields(manga!);
        embed.AddCommonMediaFieldsBottom(manga!);
        embed.AddCommonMediaEmbedProperties(manga!);
        var btn = manga!.GetSiteButton();
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed.Build()).AddComponents(btn));
    }

    private static void AddFields(this DiscordEmbedBuilder embed, Media media)
    {
        if (media.Volumes > 0)
        {
            embed.AddField("Volumes", media.Volumes.ToString(), true);
        }

        if (media.Chapters > 0)
        {
            embed.AddField("Chapters", media.Chapters.ToString(), true);
        }
    }
}