using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Amadeus.Bot.Handlers;
using Amadeus.Bot.Models;
using Amadeus.Db;
using Amadeus.Db.Helper;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Amadeus.Bot
{
    public class Program
    {
        private DiscordSocketClient _client;

        public static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            var cfg = JsonSerializer.Deserialize<AmadeusConfig>(await File.ReadAllTextAsync("config.json"));

            if (cfg != null)
                using (var services = ConfigureServices())
                {
                    _client = services.GetRequiredService<DiscordSocketClient>();

                    await ConfigHelper.LoadConfigs();
                    
                    await _client.LoginAsync(TokenType.Bot, cfg.Token);
                    await _client.StartAsync();

                    await services.GetRequiredService<CommandHandler>().InstallCommandsAsync();

                    // Block this task until the program is closed.
                    await Task.Delay(-1);
                }
        }

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandler>()
                .BuildServiceProvider();
        }
    }
}