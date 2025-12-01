using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace UUIDGenerator.Services
{
    public static class JsonExactLoader
    {
        public static JObject LoadJsonExact(string json)
        {
            using var stringReader = new StringReader(json);

            var reader = new JsonTextReader(stringReader)
            {
                DateParseHandling = DateParseHandling.None,
                FloatParseHandling = FloatParseHandling.Decimal // keeps numbers as raw decimal text
            };

            // Read entire JSON as a JToken WITHOUT modifying value formats
            JToken token = JToken.ReadFrom(reader);

            // Convert token to JObject (root must be object)
            return (JObject)token;
        }
    }
}
