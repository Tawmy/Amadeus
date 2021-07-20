using System.Threading.Tasks;
using Amadeus.Db;
using Amadeus.Db.Helper;
using NUnit.Framework;

namespace Amadeus.Tests
{
    public class ModerationTests
    {
        [Test]
        public async Task LoadConfigs()
        {
            await ConfigHelper.LoadConfigs();
            Assert.NotNull(Configuration.GuildConfigs);
        }
    }
}