using System.Collections.Generic;
using System.Linq;
using Amadeus.Db.Enums;

namespace Amadeus.Db.Statics
{
    public static class ConfigOptions
    {
        private static readonly List<ConfigOption> List;

        static ConfigOptions()
        {
            List = new List<ConfigOption>
            {
                new(1, "CommandPrefix", "todo", CsType.String, "!", 1)
            };
        }
        
        public static ConfigOption Get(int id)
        {
            return List.FirstOrDefault(x => x.Id == id);
        }

        public static ConfigOption Get(string name)
        {
            return List.FirstOrDefault(x => x.Name.Equals(name));
        }
    }

    public class ConfigOption
    {
        public readonly int ConfigOptionCategoryId;
        public readonly CsType CsType;
        public readonly string DefaultValue;
        public readonly string Description;
        public readonly int Id;
        public readonly string Name;

        public ConfigOption(int i, string n, string d, CsType t, string df, int ci)
        {
            Id = i;
            Name = n;
            Description = d;
            CsType = t;
            DefaultValue = df;
            ConfigOptionCategoryId = ci;
        }
    }
}