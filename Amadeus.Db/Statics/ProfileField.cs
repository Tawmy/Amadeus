using System.Collections.Generic;
using Amadeus.Db.BaseClasses;

namespace Amadeus.Db.Statics
{
    public class ProfileFields : DbClass<ProfileField>
    {
        public ProfileFields() : base(new List<ProfileField>
        {
            new(1, 1, "Twitter", true, 1),
            new(2, 1, "Steam", true, 2)
        })
        {
        }
    }

    public class ProfileField : DbField
    {
        public readonly bool HasUrl;

        public readonly int ProfileFieldCategoryId;

        public ProfileField(int i, int s, string n, bool u, int ci) : base(i, s, n)
        {
            HasUrl = u;
            ProfileFieldCategoryId = ci;
        }
    }
}