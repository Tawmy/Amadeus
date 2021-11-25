using Amadeus.Db.Helper;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Amadeus.Bot.Commands.ModerationModule;

public static class VerifyCommand
{
    public static async Task RunMenu(ContextMenuContext ctx)
    {
        await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

        if (ctx.TargetMember == null)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Member not found."));
            return;
        }

        var role = await ConfigHelper.GetRole("Verification Role", ctx.Guild);
        if (role == null)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Verification role not found."));
            return;
        }

        if (ctx.TargetMember?.Roles.Contains(role) == true)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(
                $"{ctx.TargetMember.Nickname ?? ctx.TargetMember.Username} is already verified."));
            return;
        }

        var embed = await GrantRoleAndGetEmbed(ctx.TargetMember!, ctx.Member, role);
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }

    public static async Task RunSlash(InteractionContext ctx, DiscordUser user)
    {
        await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource);

        var member = user as DiscordMember;

        if (member == null)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Member not found."));
            return;
        }
        
        var role = await ConfigHelper.GetRole("Verification Role", ctx.Guild);
        if (role == null)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Verification role not found."));
            return;
        }
        
        if (member.Roles.Contains(role))
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent(
                $"{member.Nickname ?? member.Username} is already verified."));
            return;
        }

        var embed = await GrantRoleAndGetEmbed(member, ctx.Member, role);
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed));
    }

    private static async Task<DiscordEmbed> GrantRoleAndGetEmbed(DiscordMember targetMember, DiscordMember actingMember, DiscordRole role)
    {
        await targetMember.GrantRoleAsync(role,
            $"Verification by {actingMember.Username}#{actingMember.Discriminator}")!;
        
        var embed = new DiscordEmbedBuilder
        {
            Title = targetMember.Nickname ?? targetMember.Username,
            Description = targetMember.Mention
        };
        embed.WithAuthor($"{actingMember.Nickname ?? actingMember.Username} verified:",
            iconUrl: actingMember.AvatarUrl);
        embed.WithThumbnail(targetMember.AvatarUrl);
        embed.WithColor(DiscordColor.Blurple);
        return embed.Build();
    }
}