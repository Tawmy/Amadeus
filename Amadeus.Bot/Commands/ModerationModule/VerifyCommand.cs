using Amadeus.Db.Helper;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Amadeus.Bot.Commands.ModerationModule;

public static class VerifyCommand
{
    public static async Task Run(ContextMenuContext ctx)
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

        await ctx.TargetMember?.GrantRoleAsync(role,
            $"Verification by {ctx.Member.Username}#{ctx.Member.Discriminator}")!;

        var embed = new DiscordEmbedBuilder
        {
            Title = ctx.TargetMember.Nickname ?? ctx.TargetMember.Username,
            Description = ctx.TargetMember.Mention
        };
        embed.WithAuthor($"{ctx.Member.Nickname ?? ctx.Member.Username} verified:",
            iconUrl: ctx.Member.AvatarUrl);
        embed.WithThumbnail(ctx.TargetMember.AvatarUrl);
        embed.WithColor(DiscordColor.Blurple);
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embed.Build()));
    }
}