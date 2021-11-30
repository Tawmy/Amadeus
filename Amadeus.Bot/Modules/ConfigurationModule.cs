using Amadeus.Bot.Checks;
using Amadeus.Bot.Commands.ConfigurationModule;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace Amadeus.Bot.Modules;

public class ConfigurationModule : ApplicationCommandModule
{
    [SlashCommand("setup", "Runs initial configuration for the bot")]
    [SlashRequireGuild]
    [GuildOwnerCheck]
    public async Task SlashSetup(InteractionContext ctx,
        [Option("ModeratorRole", "todo")] DiscordRole roleModerator,
        [Option("ModeratorChannel", "todo")] DiscordChannel channelModerator,
        [Option("LogChannel", "todo")] DiscordChannel channelLog,
        [Option("ArchiveChannel", "todo")] DiscordChannel channelArchive)
    {
        await new SetupCommand(ctx, roleModerator, channelModerator, channelLog, channelArchive).RunSlash();
    }
}