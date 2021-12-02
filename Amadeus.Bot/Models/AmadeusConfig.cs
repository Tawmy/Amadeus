namespace Amadeus.Bot.Models;

public class AmadeusConfig
{
    public string Token { get; set; } = null!;
    public string DbString { get; set; } = null!;
    public ulong MainChannelId { get; set; }
}