using System.Collections.Generic;
using System.Linq;

namespace Amadeus.Db.Statics
{
    public static class ProfileFieldCategories
    {
        private static readonly List<ProfileFieldCategory> List;

        static ProfileFieldCategories()
        {
            List = new List<ProfileFieldCategory>
            {
                new(1, 1, "Social", "todo"),
                new(2, 2, "Gaming", "todo")
            };
        }

        public static ProfileFieldCategory Get(int id)
        {
            return List.First(x => x.Id == id);
        }

        public static ProfileFieldCategory Get(string name)
        {
            return List.First(x => x.Name.Equals(name));
        }

        public static List<ProfileFieldCategory> Get(IEnumerable<int> ids)
        {
            return List.Where(x => ids.Contains(x.Id)).OrderBy(x => x.SortId).ToList();
        }
    }

    public class ProfileFieldCategory
    {
        public readonly int Id;
        public readonly int SortId;
        public readonly string Name;
        public readonly string Description;

        public ProfileFieldCategory(int i, int s, string n, string d)
        {
            Id = i;
            SortId = s;
            Name = n;
            Description = d;
        }
    }
}