namespace Amadeus.Db.Models;

public class CommandConfigDiscordEntityAssignment
{
    public int Id { get; set; }

    public int CommandConfigId { get; set; }
    public CommandConfig CommandConfig { get; set; } = null!;

    public ulong DiscordEntityId { get; set; }
    public DiscordEntity DiscordEntity { get; set; } = null!;

    public bool IsBlacklist { get; set; }
}