using Amadeus.Db.Helper;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Amadeus.Bot.Checks;

public class ModeratorSlashAttribute : SlashCheckBaseAttribute
{
    public InteractionContext? Ctx;
    public DiscordRole? ModeratorRole;
    
    public override async Task<bool> ExecuteChecksAsync(InteractionContext ctx)
    {
        Ctx = ctx;
        
        if (ctx.Guild == null) return true;
        if (ctx.Member == null) return false;

        ModeratorRole = await ConfigHelper.GetRole("Moderator Role", ctx.Guild);
        return ModeratorRole != null && ctx.Member.Roles.Contains(ModeratorRole);
    }
}

public class ModeratorMenuAttribute : ContextMenuCheckBaseAttribute
{
    public ContextMenuContext? Ctx;
    public DiscordRole? ModeratorRole;
    public override async Task<bool> ExecuteChecksAsync(ContextMenuContext ctx)
    {
        Ctx = ctx;
        
        if (ctx.Guild == null) return true;
        if (ctx.Member == null) return false;

        ModeratorRole = await ConfigHelper.GetRole("Moderator Role", ctx.Guild);
        return ModeratorRole != null && ctx.Member.Roles.Contains(ModeratorRole);
    }
}