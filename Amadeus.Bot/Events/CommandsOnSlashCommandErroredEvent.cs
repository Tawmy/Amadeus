using Amadeus.Bot.Handler;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;

namespace Amadeus.Bot.Events;

public class CommandsOnSlashCommandErroredEvent
{
    public static async Task CommandsOnSlashCommandErrored(SlashCommandsExtension sender, SlashCommandErrorEventArgs e)
    {
        if (e.Exception is SlashExecutionChecksFailedException ex)
            await SlashExecutionChecksFailedExceptionHandler.HandleException(e, ex);
    }
}