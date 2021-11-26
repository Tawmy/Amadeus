using Amadeus.Db.Enums;
using Amadeus.Db.Statics.BaseClasses;

namespace Amadeus.Db.Statics;

public class ConfigOptions : StaticClass<ConfigOption>
{
    public ConfigOptions() : base(new List<ConfigOption>
    {
        new(1, 1, "Moderator Role", "todo", ConfigType.Role, null, 2),
        new(2, 2, "Verification Role", "todo", ConfigType.Role, null, 2),
        new(3, 1, "Moderator Channel", "todo", ConfigType.Channel, null, 3),
        new(4, 3, "Archive Channel", "todo", ConfigType.Channel, null, 3)
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

    public ConfigOptionCategory ConfigOptionCategory => new ConfigOptionCategories().Get(ConfigOptionCategoryId);

    public ConfigOption(int i, int s, string n, string d, ConfigType t, string df, int ci) : base(i, s, n)
    {
        Description = d;
        Type = t;
        DefaultValue = df;
        ConfigOptionCategoryId = ci;
    }
}