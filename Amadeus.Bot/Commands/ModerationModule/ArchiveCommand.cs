using Amadeus.Bot.Extensions;
using Amadeus.Bot.Resources;
using Amadeus.Db.Helper;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;

namespace Amadeus.Bot.Commands.ModerationModule;

public class ArchiveCommand
{
    private readonly DiscordChannel _channel;
    private readonly InteractionContext _ctx;
    private readonly int _maxMsgs;

    public ArchiveCommand(InteractionContext ctx, DiscordChannel channel, long maxMsgs)
    {
        _ctx = ctx;
        _channel = channel;
        _maxMsgs = Convert.ToInt32(maxMsgs > 1000 ? 1000 : maxMsgs);
        SiteStr = ArchiveParts.GetHeaderAndBodyPartOne(_channel);
    }

    private string SiteStr { get; set; }

    public async Task RunSlash()
    {
        // Prevent categories from being archived
        if (_channel.IsCategory)
        {
            await _ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder().WithContent("Cannot archive a category."));
            return;
        }

        await _ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

        var canDelete = (_channel.PermissionsFor(_ctx.Guild.CurrentMember) & Permissions.ManageMessages) != 0;

        var logChannel = await ConfigHelper.GetChannel("Archive Channel", _channel.Guild);
        if (logChannel == null)
        {
            await _ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Archive channel not found."));
            return;
        }

        // Get most recent N messages, filter out bot messages and empty objects
        var msgs = await _channel.GetMessagesAsync(_maxMsgs);
        msgs = msgs.Where(x => x != null).ToList();
        if (msgs.Count == 0)
        {
            await _ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("No messages found"));
            return;
        }

        FillSiteStr(msgs);

        await PostFileAndDetailedEmbedInLogChannel(msgs, logChannel);

        // Post simple response in channel where command was run
        var responseString = $"{msgs.Count} messages from {_channel.Mention} archived into {logChannel.Mention}.";

        var webhookBuilder = new DiscordWebhookBuilder().WithContent(responseString);

        if (canDelete)
        {
            var delButton = new DiscordButtonComponent(ButtonStyle.Danger, "delete", "Delete archived messages",
                emoji: new DiscordComponentEmoji("🗑️"));
            var doneButton = new DiscordButtonComponent(ButtonStyle.Secondary, "done", "Done");
            webhookBuilder.AddComponents(delButton, doneButton);
        }

        var botMsg = await _ctx.EditResponseAsync(webhookBuilder);

        // End execution here if bot does not have permissions to manage messages
        if (!canDelete) return;
        await HandleDeletion(botMsg, msgs, responseString);
    }

    private void FillSiteStr(IReadOnlyList<DiscordMessage> msgs)
    {
        // Add author header
        SiteStr = msgs.Where(x => x.Author != null && !x.Author.IsBot)
            .Select(x => x.Author)
            .Distinct()
            .Aggregate(SiteStr, (current, author) => current + ArchiveParts.GetAuthorHtmlString(author));

        SiteStr += ArchiveParts.GetBridgeHtmlString();

        // Add all the messages to the string
        SiteStr = msgs.Reverse()
            .Aggregate(SiteStr, (current, msg) => current + ArchiveParts.GetMessageHtmlString(msg));

        SiteStr += ArchiveParts.GetEndingHtmlString();
    }

    private async Task PostFileAndDetailedEmbedInLogChannel(IReadOnlyCollection<DiscordMessage> msgs,
        DiscordChannel logChannel)
    {
        // Post file and detailed embed in log channel
        var embed = GetEmbed(msgs.Count, msgs.Select(x => x.Author).Distinct().ToList());
        var filename = $"{_channel.Name}_{DateTime.Now:yy-MM-dd-HH-mm}.html";
        await using var stream = SiteStr.GenerateStream();
        var msg = new DiscordMessageBuilder().WithEmbed(embed).WithFile(filename, stream);
        await logChannel.SendMessageAsync(msg);
    }

    private DiscordEmbed GetEmbed(int msgs, IReadOnlyCollection<DiscordUser> users)
    {
        var embed = new DiscordEmbedBuilder();
        embed.WithAuthor($"#{_channel.Name}", $"https://discordapp.com/channels/{_channel.GuildId}/{_channel.Id}");
        embed.AddField("Messages", msgs.ToString(), true);
        embed.AddField("Users", users.Count.ToString(), true);
        embed.AddField("List", string.Join(Environment.NewLine, users.Select(x => x.Mention)));
        return embed.Build();
    }

    private static async Task UpdateMessageWithoutButtons(BaseContext ctx, string responseString)
    {
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(responseString));
    }

    private async Task HandleDeletion(DiscordMessage botMsg, IEnumerable<DiscordMessage> msgs, string responseString)
    {
        var result = await botMsg.WaitForButtonAsync();

        if (result.TimedOut || result.Result.Id.Equals("done"))
        {
            // dumb workaround until i figure out something better
            await UpdateMessageWithoutButtons(_ctx, responseString);
            return;
        }

        await _channel.DeleteMessagesAsync(msgs);
        await UpdateMessageWithoutButtons(_ctx, $"{responseString}{Environment.NewLine}Messages were deleted.");
    }
}