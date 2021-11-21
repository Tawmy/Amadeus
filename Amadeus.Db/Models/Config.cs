using System.ComponentModel.DataAnnotations.Schema;
using Amadeus.Db.Statics;

namespace Amadeus.Db.Models;

public class Config
{
    public int Id { get; set; }

    public int ConfigOptionId { get; set; } // statics
    [NotMapped] ConfigOption ConfigOption => new ConfigOptions().Get(ConfigOptionId);

    public ulong GuildId { get; set; }
    public Guild Guild { get; set; }

    public string Value { get; set; }
}