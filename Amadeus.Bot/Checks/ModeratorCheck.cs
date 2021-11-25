using Amadeus.Db.Helper;
using DSharpPlus.SlashCommands;

namespace Amadeus.Bot.Checks;

public class ModeratorSlashAttribute : SlashCheckBaseAttribute
{
    public InteractionContext? Ctx;
    
    public override async Task<bool> ExecuteChecksAsync(InteractionContext ctx)
    {
        Ctx = ctx;
        
        if (ctx.Guild == null) return true;
        if (ctx.Member == null) return false;

        var role = await ConfigHelper.GetRole("Moderator Role", ctx.Guild);
        return role != null && ctx.Member.Roles.Contains(role);
    }
}

public class ModeratorMenuAttribute : ContextMenuCheckBaseAttribute
{
    public ContextMenuContext? Ctx;
    public override async Task<bool> ExecuteChecksAsync(ContextMenuContext ctx)
    {
        Ctx = ctx;
        
        if (ctx.Guild == null) return true;
        if (ctx.Member == null) return false;

        var role = await ConfigHelper.GetRole("Moderator Role", ctx.Guild);
        return role != null && ctx.Member.Roles.Contains(role);
    }
}