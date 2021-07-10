using System.Threading.Tasks;
using Amadeus.Db;
using Amadeus.Db.Models;
using logm.EntityRepository.Core;
using NUnit.Framework;

namespace Amadeus.Tests
{
    public class DatabaseTests
    {
        [Test]
        public async Task TestConfigOptions()
        {
            var allAsync = await EntityRepository<AmadeusContext, ConfigOption>.GetAllAsync();
            var all = EntityRepository<AmadeusContext, ConfigOption>.GetAll();
        }
    }
}