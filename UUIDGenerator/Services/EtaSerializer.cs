using Newtonsoft.Json.Linq;
using System.Text;

namespace UUIDGenerator.Services
{
    public static class EtaSerializer
    {
        public static string Serialize(JToken token, string parentName = "")
        {
            StringBuilder result = new();

            if (token is JObject obj)
            {
                foreach (var property in obj.Properties())
                {
                    // DO NOT serialize UUID
                    //if (property.Name.Equals("uuid", StringComparison.OrdinalIgnoreCase))
                    //    continue;

                    string name = property.Name.ToUpperInvariant();
                    result.Append($"\"{name}\"");
                    result.Append(Serialize(property.Value, name));
                }
            }
            else if (token is JArray arr)
            {
                foreach (var item in arr)
                {
                    result.Append($"\"{parentName}\"");
                    result.Append(Serialize(item, parentName));
                }
            }
            else if (token is JValue val)
            {
                // VALUE IS EXACT STRING FROM JsonExactLoader
                string raw = val.Value?.ToString() ?? "";
                result.Append($"\"{raw}\"");
            }
            else if (token is JToken strToken)
            {
                // Strings from LoadJsonExact arrive as JValue
                result.Append($"\"{strToken.ToString()}\"");
            }

            return result.ToString();
        }
    }
}
