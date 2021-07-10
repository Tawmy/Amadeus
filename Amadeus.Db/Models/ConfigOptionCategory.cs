using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Amadeus.Db.Models
{
    public class ConfigOptionCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        [JsonIgnore] public ICollection<ConfigOption> ConfigOptions { get; set; }
    }
}