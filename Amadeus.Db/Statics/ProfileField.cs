using System.Collections.Generic;
using System.Linq;

namespace Amadeus.Db.Statics
{
    public static class ProfileFields
    {
        private static readonly List<ProfileField> List;

        static ProfileFields()
        {
            List = new List<ProfileField>
            {
                new(1, 1, "Twitter", true, 1),
                new(2, 1, "Steam", true, 2)
            };
        }

        public static ProfileField Get(int id)
        {
            return List.First(x => x.Id == id);
        }

        public static ProfileField Get(string name)
        {
            return List.First(x => x.Name.Equals(name));
        }

        public static List<ProfileField> Get(IEnumerable<int> ids)
        {
            return List.Where(x => ids.Contains(x.Id)).OrderBy(x => x.SortId).ToList();
        }
    }

    public class ProfileField
    {
        public readonly int Id;
        public readonly int SortId;
        public readonly string Name;
        public readonly bool HasUrl;

        public readonly int ProfileFieldCategoryId;

        public ProfileField(int i, int s, string n, bool u, int ci)
        {
            Id = i;
            SortId = s;
            Name = n;
            HasUrl = u;
            ProfileFieldCategoryId = ci;
        }
    }
}