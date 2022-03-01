using Amadeus.Bot.Handler;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace Amadeus.Bot.Events;

public static class ComponentInteractionCreatedEvent
{
    public static async Task ClientOnComponentInteractionCreated(DiscordClient sender,
        ComponentInteractionCreateEventArgs e)
    {
        var info = e.Id.Split("_");
        int? secondaryInfo = info.Length > 1 && int.TryParse(info[1], out var result) ? result : null;

        switch (info[0])
        {
            case "selfAssignButton":
                if (secondaryInfo == null) return;
                await SelfAssignRolesHandler.ShowRoleSelection(sender, e, secondaryInfo.Value);
                break;
            case "selfAssignDropdown":
                if (secondaryInfo == null) return;
                await SelfAssignRolesHandler.AssignRoles(sender, e, secondaryInfo.Value);
                break;
        }
    }
}