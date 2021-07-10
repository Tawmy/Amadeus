using System.Threading.Tasks;
using Amadeus.Db.Helper;
using NUnit.Framework;

namespace Amadeus.Tests
{
    public class ConfigTests
    {
        [Test]
        public async Task GetCommandPrefixAsString()
        {
            var prefix = await ConfigHelper.GetString("CommandPrefix");
            Assert.AreEqual(prefix, "!");
        }

        [Test]
        public async Task GetCommandPrefixAsChar()
        {
            var prefix = await ConfigHelper.GetChar("CommandPrefix");
            Assert.AreEqual(prefix, '!');
        }
    }
}