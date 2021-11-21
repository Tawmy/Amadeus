using Amadeus.Db.Enums;
using Amadeus.Db.Statics.BaseClasses;

namespace Amadeus.Db.Statics;

public class ConfigOptions : StaticClass<ConfigOption>
{
    public ConfigOptions() : base(new List<ConfigOption>
    {
        new(1, 1, "CommandPrefix", "todo", ConfigType.String, "!", 1),
        new(2, 1, "Verification Role", "todo", ConfigType.Role, null, 2),
        new(3, 1, "Mod Channel", "todo", ConfigType.Channel, null, 3),
        new(4, 1, "Log Channel", "todo", ConfigType.Channel, null, 3)
    })
    {
    }
}

public class ConfigOption : StaticField
{
    public readonly int ConfigOptionCategoryId;
    public readonly string DefaultValue;
    public readonly string Description;
    public readonly ConfigType Type;

    public ConfigOption(int i, int s, string n, string d, ConfigType t, string df, int ci) : base(i, s, n)
    {
        Description = d;
        Type = t;
        DefaultValue = df;
        ConfigOptionCategoryId = ci;
    }
}