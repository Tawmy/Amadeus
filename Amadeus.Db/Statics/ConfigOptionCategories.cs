using System.Collections.Generic;
using System.Linq;
using Amadeus.Db.Enums;

namespace Amadeus.Db.Statics
{
    public class ConfigOptionCategories
    {
        private static readonly List<ConfigOptionCategory> List;

        static ConfigOptionCategories()
        {
            List = new List<ConfigOptionCategory>
            {
                new(1, "General", "todo")
            };
        }
        
        public static ConfigOptionCategory Get(int id)
        {
            return List.FirstOrDefault(x => x.Id == id);
        }

        public static ConfigOptionCategory Get(string name)
        {
            return List.FirstOrDefault(x => x.Name.Equals(name));
        }
    }

    public class ConfigOptionCategory
    {
        public readonly int Id;
        public readonly string Name;
        public readonly string Description;

        public ConfigOptionCategory(int i, string n, string d)
        {
            Id = i;
            Name = n;
            Description = d;
        }
    }
}