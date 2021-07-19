using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Amadeus.Db.Models
{
    public class User
    {
        public ulong Id { get; set; }

        [JsonIgnore] public ICollection<ProfileEntry> ProfileEntries { get; set; }
    }
}