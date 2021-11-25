using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;

namespace Amadeus.Bot.Errors;

public static class ErrorHandler
{
    public static async Task CommandsOnSlashCommandErrored(SlashCommandsExtension sender, SlashCommandErrorEventArgs e)
    {
        if (e.Exception is SlashExecutionChecksFailedException ex)
        {
            await SlashExecutionChecksFailedExceptionHandler.HandleException(e, ex);
        }
    }
}