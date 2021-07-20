using System.Collections.Generic;
using Amadeus.Db.BaseClasses;

namespace Amadeus.Db.Statics
{
    public class ConfigOptionCategories : DbClass<ConfigOptionCategory>
    {
        public ConfigOptionCategories() : base(new List<ConfigOptionCategory>
        {
            new(1, 1, "General", "todo"),
            new(2, 2, "Roles", "todo"),
            new(2, 3, "Channels", "todo")
        })
        {
        }
    }

    public class ConfigOptionCategory : DbField
    {
        public readonly string Description;

        public ConfigOptionCategory(int i, int s, string n, string d) : base(i, s, n)
        {
            Description = d;
        }
    }
}