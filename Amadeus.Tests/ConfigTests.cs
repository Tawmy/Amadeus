using System.Threading.Tasks;
using Amadeus.Db;
using Amadeus.Db.Helper;
using NUnit.Framework;

namespace Amadeus.Tests
{
    public class ConfigTests
    {
        [Test]
        public async Task LoadConfigs()
        {
            await ConfigHelper.LoadConfigs();
            Assert.NotNull(Configuration.GuildConfigs);
        }

        [Test]
        public async Task GetCommandPrefixAsString()
        {
            await ConfigHelper.LoadConfigs();
            var prefix = await ConfigHelper.GetString("CommandPrefix");
            Assert.AreEqual(prefix, "!");
        }

        [Test]
        public async Task GetCommandPrefixAsStringForGuild()
        {
            await ConfigHelper.LoadConfigs();
            var prefix = await ConfigHelper.GetString("CommandPrefix", 640467169733246976);
            Assert.AreEqual(prefix, "!");
        }

        [Test]
        public async Task GetCommandPrefixAsChar()
        {
            await ConfigHelper.LoadConfigs();
            var prefix = await ConfigHelper.GetChar("CommandPrefix");
            Assert.AreEqual(prefix, '!');
        }

        [Test]
        public async Task SetPrefixForTestGuild()
        {
            await ConfigHelper.LoadConfigs();
            var result = await ConfigHelper.Set("CommandPrefix", 640467169733246976, '!');
            Assert.IsTrue(result);
        }
    }
}