
using System.Text.Json.Serialization;
using System.Collections.Generic;


namespace SantasLittleHelper.Datamodels
{
    class CustomJWT
    {
        [JsonPropertyName("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")]
        public string email { get; set; }
        public List<string> groups { get; set; } = new List<string>();
        public int exp { get; set; }
        public string iss { get; set; }
        public string aud { get; set; }
    }
}