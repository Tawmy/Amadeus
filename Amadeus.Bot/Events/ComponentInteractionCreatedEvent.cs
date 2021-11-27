using Amadeus.Bot.Handler;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace Amadeus.Bot.Events;

public static class ComponentInteractionCreatedEvent
{
    public static async Task ClientOnComponentInteractionCreated(DiscordClient sender, ComponentInteractionCreateEventArgs e)
    {
        switch (e.Id)
        {
            case "selfAssignButton":
                await SelfAssignRolesHandler.ShowRoleSelection(sender, e);
                break;
            case "selfAssignDropdown":
                await SelfAssignRolesHandler.AssignRoles(sender, e);
                break;
        }
    }
}