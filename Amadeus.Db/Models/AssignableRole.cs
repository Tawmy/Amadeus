namespace Amadeus.Db.Models;

public class AssignableRole
{
    public ulong Id { get; set; }
    
    public ulong GuildId { get; set; }
    public Guild Guild { get; set; }
}