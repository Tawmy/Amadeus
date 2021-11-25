using Amadeus.Db.Helper;
using DSharpPlus.SlashCommands;

namespace Amadeus.Bot.Checks;

public class ModeratorSlashAttribute : SlashCheckBaseAttribute
{
    public override async Task<bool> ExecuteChecksAsync(InteractionContext ctx)
    {
        if (ctx.Guild == null) return true;
        if (ctx.Member == null) return false;

        var role = await ConfigHelper.GetRole("Moderator Role", ctx.Guild);
        return role != null && ctx.Member.Roles.Contains(role);
    }
}

public class ModeratorMenuAttribute : ContextMenuCheckBaseAttribute
{
    public override async Task<bool> ExecuteChecksAsync(ContextMenuContext ctx)
    {
        if (ctx.Guild == null) return true;
        if (ctx.Member == null) return false;

        var role = await ConfigHelper.GetRole("Moderator Role", ctx.Guild);
        return role != null && ctx.Member.Roles.Contains(role);
    }
}