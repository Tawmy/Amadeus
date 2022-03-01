using Amadeus.Db.Helper;
using Amadeus.Db.Models;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;

namespace Amadeus.Bot.Commands.RolesModule;

public static class PostRolesMenuCommand
{
    public static async Task RunSlash(InteractionContext ctx, DiscordChannel? targetChannel)
    {
        await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

        // get channel from context if user did not provide one (it's an optional command parameter)
        targetChannel = targetChannel != null ? targetChannel : ctx.Channel;
        
        // get list of menus
        var menus = await RolesHelper.GetSelfAssignMenus(ctx.Guild);
        if (menus.Count == 0)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("No menus defined."));
            return;
        }
        
        // show user selection of menus
        var promptEmbed = GetPromptEmbed(targetChannel);
        var selectMenu = GetSelfAssignMenuComponent(menus);
        var botMsg =
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(promptEmbed).AddComponents(selectMenu));

        // wait for user to select a menu from the list
        var result = await botMsg.WaitForSelectAsync(ctx.User, selectMenu.CustomId);
        if (result.TimedOut)
        {
            await botMsg.DeleteAsync();
            return;
        }
        
        // post menu as soon as user has selected it and show confirmation
        var chosenMenu = menus.First(x => x.Id == Convert.ToInt32(result.Result.Values[0]));
        
        var confirmEmbed = await PostMenu(chosenMenu, targetChannel)
            ? GetSuccessEmbed(chosenMenu, targetChannel)
            : GetErrorEmbed(chosenMenu, targetChannel);
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(confirmEmbed));
    }

    private static DiscordEmbed GetPromptEmbed(DiscordChannel targetChannel)
    {
        var promptEmbed = new DiscordEmbedBuilder();
        promptEmbed.WithTitle("Select Roles Menu");
        promptEmbed.WithDescription(
            $"Select a menu to be posted in {targetChannel.Mention}. It will be posted as soon as it is selected.");
        return promptEmbed.Build();
    }

    private static DiscordSelectComponent GetSelfAssignMenuComponent(IEnumerable<SelfAssignMenu> menus)
    {
        var options = menus.Select(x => new DiscordSelectComponentOption(x.Title, x.Id.ToString(), x.Description));
        const string selectMenuId = "selfAssignMenuSelect";
        return new DiscordSelectComponent(selectMenuId, "Select menu", options, minOptions: 1,
            maxOptions: 1);
    }

    private static async Task<bool> PostMenu(SelfAssignMenu chosenMenu, DiscordChannel targetChannel)
    {
        var embed = new DiscordEmbedBuilder();
        embed.WithTitle(chosenMenu.Title);
        embed.WithDescription(chosenMenu.Description);
        var btn = new DiscordButtonComponent(ButtonStyle.Primary, $"selfAssignButton_{chosenMenu.Id}", "Show roles");
        return await targetChannel.SendMessageAsync(new DiscordMessageBuilder().AddEmbed(embed.Build()).AddComponents(btn)) != null;
    }

    private static DiscordEmbed GetSuccessEmbed(SelfAssignMenu chosenMenu, DiscordChannel targetChannel)
    {
        var embed = new DiscordEmbedBuilder();
        embed.WithTitle("Menu posted");
        embed.WithDescription($"The menu {chosenMenu.Title} has been posted in {targetChannel.Mention}.");
        embed.WithColor(DiscordColor.SpringGreen);
        return embed.Build();
    }
    
    private static DiscordEmbed GetErrorEmbed(SelfAssignMenu chosenMenu, DiscordChannel targetChannel)
    {
        var embed = new DiscordEmbedBuilder();
        embed.WithTitle("Menu failed to post");
        embed.WithDescription($"The menu {chosenMenu.Title} could not be posted in {targetChannel.Mention}.");
        embed.WithColor(DiscordColor.Red);
        return embed.Build();
    }
}