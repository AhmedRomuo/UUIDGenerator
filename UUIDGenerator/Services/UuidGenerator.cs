using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;

namespace UUIDGenerator.Services
{
    public static class UuidGenerator
    {
        public static string ComputeUUID(string json)
        {
            // Load JSON without modifying ANY values
            JObject obj = JsonExactLoader.LoadJsonExact(json);

            // Remove UUID from hashing
            if (obj["header"] is JObject hdr && hdr["uuid"] != null)
                hdr["uuid"] = "";

            // Serialize to ETA canonical string
            string normalized = EtaSerializer.Serialize(obj);

            // Compute SHA256
            using SHA256 sha = SHA256.Create();
            byte[] hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(normalized));

            StringBuilder sb = new();
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }
    }
}
