using System.Collections.Generic;
using Amadeus.Db.BaseClasses;

namespace Amadeus.Db.Statics
{
    public class ProfileFieldCategories : DbClass<ProfileFieldCategory>
    {
        public ProfileFieldCategories() : base(new List<ProfileFieldCategory>
        {
            new(1, 1, "Social", "todo"),
            new(2, 2, "Gaming", "todo")
        })
        {
        }
    }

    public class ProfileFieldCategory : DbField
    {
        public readonly string Description;

        public ProfileFieldCategory(int i, int s, string n, string d) : base(i, s, n)
        {
            Description = d;
        }
    }
}