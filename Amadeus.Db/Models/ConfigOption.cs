using System.Collections.Generic;
using System.Text.Json.Serialization;
using Amadeus.Db.Enums;

namespace Amadeus.Db.Models
{
    public class ConfigOption
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public CsType CsType { get; set; }
        public string DefaultValue { get; set; }

        public int ConfigOptionCategoryId { get; set; }
        public ConfigOptionCategory ConfigOptionCategory { get; set; }

        [JsonIgnore] public ICollection<Config> Configs { get; set; }
    }
}