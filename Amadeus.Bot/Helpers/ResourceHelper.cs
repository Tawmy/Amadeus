using System.IO;
using System.Reflection;

namespace Amadeus.Bot.Helpers
{
    public static class ResourceHelper
    {
        public static Stream GetResource(string fileName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream($"Amadeus.Bot.Resources.{fileName}");
        }
    }
}