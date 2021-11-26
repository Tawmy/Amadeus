using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;

namespace Amadeus.Bot.Handler.Errors;

public static class ErrorHandler
{
    public static async Task CommandsOnSlashCommandErrored(SlashCommandsExtension sender, SlashCommandErrorEventArgs e)
    {
        if (e.Exception is SlashExecutionChecksFailedException ex)
        {
            await SlashExecutionChecksFailedExceptionHandler.HandleException(e, ex);
        }
    }
    
    public static async Task CommandsOnContextMenuErrored(SlashCommandsExtension sender, ContextMenuErrorEventArgs e)
    {
        if (e.Exception is ContextMenuExecutionChecksFailedException ex)
        {
            await ContextMenuExecutionChecksFailedExceptionHandler.HandleException(e, ex);
        }
    }
}