using System.Collections.Generic;
using Amadeus.Db.BaseClasses;
using Amadeus.Db.Enums;

namespace Amadeus.Db.Statics
{
    public class ConfigOptions : DbClass<ConfigOption>
    {
        public ConfigOptions() : base(new List<ConfigOption>
        {
            new(1, 1, "CommandPrefix", "todo", Type.String, "!", 1),
            new(2, 1, "Verification Role", "todo", Type.Role, null, 2),
            new(3, 1, "Mod Channel", "todo", Type.Channel, null, 3),
            new(4, 1, "Log Channel", "todo", Type.Channel, null, 3)
        })
        {
        }
    }

    public class ConfigOption : DbField
    {
        public readonly int ConfigOptionCategoryId;
        public readonly string DefaultValue;
        public readonly string Description;
        public readonly Type Type;

        public ConfigOption(int i, int s, string n, string d, Type t, string df, int ci) : base(i, s, n)
        {
            Description = d;
            Type = t;
            DefaultValue = df;
            ConfigOptionCategoryId = ci;
        }
    }
}