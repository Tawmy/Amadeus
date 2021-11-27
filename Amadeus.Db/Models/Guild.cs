using System.Text.Json.Serialization;

namespace Amadeus.Db.Models;

public class Guild
{
    public ulong Id { get; set; }

    [JsonIgnore] public ICollection<AssignableRole> AssignableRoles { get; set; }
    [JsonIgnore] public ICollection<Config> Configs { get; set; }
}