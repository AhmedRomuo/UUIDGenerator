using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
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
                    string name = property.Name.ToUpperInvariant();
                    result.Append($"\"{name}\"");
                    result.Append(Serialize(property.Value, name));
                }
            }
            else if (token is JArray array)
            {
                foreach (var item in array)
                {
                    result.Append($"\"{parentName}\"");
                    result.Append(Serialize(item, parentName));
                }
            }
            else if (token is JValue value)
            {
                string strValue = value.Value?.ToString() ?? "";
                strValue = strValue.Replace("\"", "\\\"");
                result.Append($"\"{strValue}\"");
            }
            return result.ToString();
        }

        public static string ComputeUUID(JObject json)
        {
            json["uuid"] = ""; // clear before hashing
            string serialized = Serialize(json);
            using SHA256 sha256 = SHA256.Create();
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(serialized));
            StringBuilder sb = new();
            foreach (byte b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
