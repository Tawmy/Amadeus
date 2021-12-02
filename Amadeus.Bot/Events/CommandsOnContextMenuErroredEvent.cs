using Amadeus.Bot.Handler;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;

namespace Amadeus.Bot.Events;

public class CommandsOnContextMenuErroredEvent
{
    public static async Task CommandsOnContextMenuErrored(SlashCommandsExtension sender, ContextMenuErrorEventArgs e)
    {
        if (e.Exception is ContextMenuExecutionChecksFailedException ex)
            await ContextMenuExecutionChecksFailedExceptionHandler.HandleException(e, ex);
    }
}