using DSharpPlus.SlashCommands;

namespace Amadeus.Bot.Checks;

public class GuildOwnerCheck : SlashCheckBaseAttribute
{
    public InteractionContext? Ctx; // TODO implement error output for this check

    public override Task<bool> ExecuteChecksAsync(InteractionContext ctx)
    {
        Ctx = ctx;
        return Task.FromResult(ctx.Member.IsOwner);
    }
}