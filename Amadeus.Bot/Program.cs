using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Amadeus.Bot.Helpers;
using Amadeus.Bot.Models;
using Amadeus.Db.Helper;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;

namespace Amadeus.Bot
{
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

                await _amadeus.ConnectAsync();
                var hlp = new StartupHelper(_amadeus, _cfg);
                await hlp.SendStartupMessage();
                RegisterCommands();
                // Block this task until the program is closed.
                await Task.Delay(-1);
            }
        }

        private DiscordClient InitAmadeus()
        {
            return new(new DiscordConfiguration
            {
                Token = _cfg.Token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All
            });
        }

        private void RegisterCommands()
        {
            var commands = _amadeus.UseCommandsNext(new CommandsNextConfiguration
            {
                PrefixResolver = ResolvePrefixAsync
            });
            commands.RegisterCommands(Assembly.GetExecutingAssembly());
        }

        private async Task<int> ResolvePrefixAsync(DiscordMessage msg)
        {
            return await CommandHelper.ResolvePrefixAsync(msg, _cfg);
        }
    }
}