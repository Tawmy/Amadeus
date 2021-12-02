namespace Amadeus.Db.Models;

public class CommandConfig
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsModule { get; set; }
    public bool IsEnabled { get; set; }

    public ICollection<CommandConfigDiscordEntityAssignment> CommandConfigDiscordEntityAssignments { get; set; } =
        null!;
}