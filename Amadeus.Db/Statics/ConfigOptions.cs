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
                new(1, 1, "CommandPrefix", "todo", CsType.String, "!", 1)
            };
        }

        public static ConfigOption Get(int id)
        {
            return List.First(x => x.Id == id);
        }

        public static ConfigOption Get(string name)
        {
            return List.First(x => x.Name.Equals(name));
        }

        public static List<ConfigOption> Get(IEnumerable<int> ids)
        {
            return List.Where(x => ids.Contains(x.Id)).OrderBy(x => x.SortId).ToList();
        }
    }

    public class ConfigOption
    {
        public readonly int Id;
        public readonly int SortId;
        public readonly string Name;
        public readonly string Description;
        public readonly CsType CsType;
        public readonly string DefaultValue;

        public readonly int ConfigOptionCategoryId;

        public ConfigOption(int i, int s, string n, string d, CsType t, string df, int ci)
        {
            Id = i;
            SortId = s;
            Name = n;
            Description = d;
            CsType = t;
            DefaultValue = df;
            ConfigOptionCategoryId = ci;
        }
    }
}