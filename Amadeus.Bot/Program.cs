using System.Text.Json;
using Amadeus.Bot.Events;
using Amadeus.Bot.Helper;
using Amadeus.Bot.Models;
using Amadeus.Bot.Modules;
using Amadeus.Db;
using Amadeus.Db.Helper;
using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;

namespace Amadeus.Bot;

public class Program
{
    private DiscordClient _amadeus = null!;
    private AmadeusConfig _cfg = null!;

    public static void Main(string[] args)
    {
        new Program().MainAsync().GetAwaiter().GetResult();
    }

    private async Task MainAsync()
    {
        _cfg = JsonSerializer.Deserialize<AmadeusConfig>(await File.ReadAllTextAsync("config.json"))!;

        if (_cfg == null) throw new InvalidOperationException("Bot cannot run without valid configuration");

        Configuration.ConnectionString = _cfg.DbString;
        await ConfigHelper.LoadGuildConfigs();
        _amadeus = InitAmadeus();
        RegisterCommands();
        RegisterInteractivity();

        await _amadeus.ConnectAsync();

        // Block this task until the program is closed.
        await Task.Delay(-1);
    }

    private DiscordClient InitAmadeus()
    {
        var client = new DiscordClient(new DiscordConfiguration
        {
            Token = _cfg.Token,
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.Guilds
                      | DiscordIntents.GuildMembers
                      | DiscordIntents.GuildMessages
        });
        client.GuildDownloadCompleted += ClientOnGuildDownloadCompleted;
        client.ComponentInteractionCreated += ComponentInteractionCreatedEvent.ClientOnComponentInteractionCreated;
        client.GuildMemberAdded += GuildMemberAddedEvent.ClientOnGuildMemberAdded;
        client.GuildMemberRemoved += GuildMemberRemovedEvent.ClientOnGuildMemberRemoved;
        client.MessageDeleted += OnMessageDeleted.ClientOnMessageDeleted;
        return client;
    }

    private void RegisterCommands()
    {
        var commands = _amadeus.UseSlashCommands();
#if DEBUG
        commands.RegisterCommands<ConfigurationModule>(640467169733246976);
        commands.RegisterCommands<ModerationModule>(640467169733246976);
        commands.RegisterCommands<RolesModule>(640467169733246976);
        commands.RegisterCommands<AniListModule>(640467169733246976);
#else
        commands.RegisterCommands<ConfigurationModule>();
        commands.RegisterCommands<ModerationModule>();
        commands.RegisterCommands<RolesModule>();
        commands.RegisterCommands<AniListModule>();
#endif
        commands.RegisterCommands<OwnerModule>(640467169733246976);

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