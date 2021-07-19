using System.Collections.Generic;
using Amadeus.Db.BaseClasses;
using Amadeus.Db.Enums;

namespace Amadeus.Db.Statics
{
    public class ConfigOptions : DbClass<ConfigOption>
    {
        public ConfigOptions() : base(new List<ConfigOption>
        {
            new(1, 1, "CommandPrefix", "todo", CsType.String, "!", 1)
        })
        {
        }
    }

    public class ConfigOption : DbField
    {
        public readonly int ConfigOptionCategoryId;
        public readonly CsType CsType;
        public readonly string DefaultValue;
        public readonly string Description;

        public ConfigOption(int i, int s, string n, string d, CsType t, string df, int ci) : base(i, s, n)
        {
            Description = d;
            CsType = t;
            DefaultValue = df;
            ConfigOptionCategoryId = ci;
        }
    }
}