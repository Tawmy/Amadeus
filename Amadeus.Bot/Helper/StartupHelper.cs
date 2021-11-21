using Amadeus.Bot.Models;
using DSharpPlus;
using DSharpPlus.Entities;

namespace Amadeus.Bot.Helper;

public class StartupHelper
{
    private readonly DiscordClient _amadeus;
    private readonly AmadeusConfig _cfg;

    public StartupHelper(DiscordClient amadeus, AmadeusConfig cfg)
    {
        _amadeus = amadeus;
        _cfg = cfg;
    }

    public async Task SendStartupMessage()
    {
        var channel = await _amadeus.GetChannelAsync(_cfg.MainChannelId);

        if (channel == null)
        {
#if DEBUG
            Console.Write("Main channel not found. Exiting.");
#endif
            Environment.Exit(0);
        }

        var embed = new DiscordEmbedBuilder {Title = "Amadeus"};
        await channel.SendMessageAsync(embed.Build());
    }
}