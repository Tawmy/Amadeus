using System.ComponentModel.DataAnnotations.Schema;
using Amadeus.Db.Statics;

namespace Amadeus.Db.Models
{
    public class ProfileEntry
    {
        public int Id { get; set; }

        public int ProfileFieldId { get; set; } // statics
        [NotMapped] public ProfileField ProfileField { get; set; }

        public ulong UserId { get; set; }
        public User User { get; set; }

        public string Value { get; set; }
        public string Url { get; set; }
    }
}