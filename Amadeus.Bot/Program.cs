using System.Reflection;
using System.Text.Json;
using Amadeus.Bot.Events;
using Amadeus.Bot.Helper;
using Amadeus.Bot.Models;
using Amadeus.Bot.Modules;
using Amadeus.Db.Helper;
using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;

namespace Amadeus.Bot;

public class Program
{
    private DiscordClient _amadeus;
    private AmadeusConfig _cfg;

    public static void Main(string[] args)
    {
        new Program().MainAsync().GetAwaiter().GetResult();
    }

    private async Task MainAsync()
    {
        _cfg = JsonSerializer.Deserialize<AmadeusConfig>(await File.ReadAllTextAsync("config.json"));

        if (_cfg != null)
        {
            await ConfigHelper.LoadConfigs();
            _amadeus = InitAmadeus();
            RegisterCommands();
            RegisterInteractivity();

            await _amadeus.ConnectAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }
    }

    private DiscordClient InitAmadeus()
    {
        var client = new DiscordClient(new DiscordConfiguration
        {
            Token = _cfg.Token,
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.Guilds
                      | DiscordIntents.GuildMembers
        });
        client.GuildDownloadCompleted += ClientOnGuildDownloadCompleted;
        client.GuildMemberAdded += GuildMemberAddedEvent.ClientOnGuildMemberAdded;
        client.GuildMemberRemoved += GuildMemberRemovedEvent.ClientOnGuildMemberRemoved;
        return client;
    }

    private void RegisterCommands()
    {
        var commands = _amadeus.UseSlashCommands();
#if DEBUG
        commands.RegisterCommands<ModerationModule>(640467169733246976);
        commands.RegisterCommands<RolesModule>(640467169733246976);
#else
        commands.RegisterCommands<ModerationModule>();
        commands.RegisterCommands<RolesModule>();
#endif
        commands.SlashCommandErrored += CommandsOnSlashCommandErroredEvent.CommandsOnSlashCommandErrored;
        commands.ContextMenuErrored += CommandsOnContextMenuErroredEvent.CommandsOnContextMenuErrored;
    }

    private void RegisterInteractivity()
    {
        _amadeus.UseInteractivity(new InteractivityConfiguration
        {
            Timeout = new TimeSpan(0, 0, 0, 30)
        });
    }

    private async Task ClientOnGuildDownloadCompleted(DiscordClient sender, GuildDownloadCompletedEventArgs e)
    {
        await new StartupHelper(_amadeus, _cfg).SendStartupMessage();
    }
}