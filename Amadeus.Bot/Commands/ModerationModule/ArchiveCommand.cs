using System.Runtime.InteropServices.ComTypes;
using Amadeus.Bot.Helper;
using Amadeus.Bot.Resources;
using Amadeus.Db.Helper;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;

namespace Amadeus.Bot.Commands.ModerationModule;

public static class ArchiveCommand
{
    public static async Task RunSlash(InteractionContext ctx, DiscordChannel channel, long maxMsgs)
    {
        // Prevent categories from being archived
        if (channel.IsCategory)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder().WithContent("Cannot archive a category."));
            return;
        }

        await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

        var canDelete = (channel.PermissionsFor(ctx.Guild.CurrentMember) & Permissions.ManageMessages) != 0;

        var logChannel = await ConfigHelper.GetChannel("Archive Channel", channel.Guild);
        if (logChannel == null)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Archive channel not found."));
            return;
        }

        // Max amount of messages user can archive at once is 1000
        var intMaxMsgs = Convert.ToInt32(maxMsgs > 1000 ? 1000 : maxMsgs);

        // Get most recent N messages, filter out bot messages and empty objects
        var msgs = await channel.GetMessagesAsync(intMaxMsgs);
        msgs = msgs.Where(x => x != null).ToList();
        if (msgs.Count == 0)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("No messages found"));
            return;
        }

        var siteStr = GetSiteStr(channel, msgs);

        await PostFileAndDetailedEmbedInLogChannel(channel, msgs, siteStr, logChannel);

        // Post simple response in channel where command was run
        var responseString = $"{msgs.Count} messages from {channel.Mention} archived into {logChannel.Mention}.";

        var webhookBuilder = new DiscordWebhookBuilder().WithContent(responseString);

        if (canDelete)
        {
            var delButton = new DiscordButtonComponent(ButtonStyle.Danger, "delete", "Delete archived messages",
                emoji: new DiscordComponentEmoji("🗑️"));
            var doneButton = new DiscordButtonComponent(ButtonStyle.Secondary, "done", "Done");
            webhookBuilder.AddComponents(delButton, doneButton);
        }

        var botMsg = await ctx.EditResponseAsync(webhookBuilder);

        // End execution here if bot does not have permissions to manage messages
        if (!canDelete) return;
        await HandleDeletion(ctx, channel, botMsg, msgs, responseString);
    }

    private static string GetSiteStr(DiscordChannel channel, IReadOnlyList<DiscordMessage> msgs)
    {
        // Create site string, then continuously add to it
        var siteStr = ArchiveParts.GetHeaderAndBodyPartOne(channel);

        // Add author header
        siteStr = msgs.Where(x => x.Author != null && !x.Author.IsBot)
            .Select(x => x.Author)
            .Distinct()
            .Aggregate(siteStr, (current, author) => current + ArchiveParts.GetAuthorHtmlString(author));

        siteStr += ArchiveParts.GetBridgeHtmlString();

        // Add all the messages to the string
        siteStr = msgs.Reverse().Aggregate(siteStr, (current, msg) => current + ArchiveParts.GetMessageHtmlString(msg));

        siteStr += ArchiveParts.GetEndingHtmlString();
        return siteStr;
    }

    private static async Task PostFileAndDetailedEmbedInLogChannel(DiscordChannel channel,
        IReadOnlyList<DiscordMessage> msgs,
        string siteStr, DiscordChannel logChannel)
    {
        // Post file and detailed embed in log channel
        var embed = GetEmbed(channel, msgs.Count, msgs.Select(x => x.Author).Distinct().ToList());
        var filename = $"{channel.Name}_{DateTime.Now:yy-MM-dd-HH-mm}.html";
        await using var stream = StreamHelper.GenerateStreamFromString(siteStr);
        var msg = new DiscordMessageBuilder().WithEmbed(embed).WithFile(filename, stream);
        await logChannel.SendMessageAsync(msg);
    }

    private static DiscordEmbed GetEmbed(DiscordChannel channel, int msgs, IReadOnlyCollection<DiscordUser> users)
    {
        var embed = new DiscordEmbedBuilder();
        embed.WithAuthor($"#{channel.Name}", $"https://discordapp.com/channels/{channel.GuildId}/{channel.Id}");
        embed.AddField("Messages", msgs.ToString(), true);
        embed.AddField("Users", users.Count.ToString(), true);
        embed.AddField("List", string.Join(Environment.NewLine, users.Select(x => x.Mention)));
        return embed.Build();
    }

    private static async Task UpdateMessageWithoutButtons(BaseContext ctx, string responseString)
    {
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(responseString));
    }

    private static async Task HandleDeletion(BaseContext ctx, DiscordChannel channel, DiscordMessage botMsg,
        IEnumerable<DiscordMessage> msgs, string responseString)
    {
        var result = await botMsg.WaitForButtonAsync();

        if (result.TimedOut || result.Result.Id.Equals("done"))
        {
            // dumb workaround until i figure out something better
            await UpdateMessageWithoutButtons(ctx, responseString);
            return;
        }

        await channel.DeleteMessagesAsync(msgs);
        await UpdateMessageWithoutButtons(ctx, $"{responseString}{Environment.NewLine}Messages were deleted.");
    }
}