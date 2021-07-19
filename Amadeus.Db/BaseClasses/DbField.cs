namespace Amadeus.Db.BaseClasses
{
    public class DbField
    {
        public readonly int Id;
        public readonly string Name;
        public readonly int SortId;

        public DbField(int i, int s, string n)
        {
            Id = i;
            SortId = s;
            Name = n;
        }
    }
}