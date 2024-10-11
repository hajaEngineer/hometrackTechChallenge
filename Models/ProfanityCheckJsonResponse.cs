using Newtonsoft.Json;

namespace PurgomalumAPIAutomation.Models
{
    public class ProfanityCheckJsonResponse
    {
        [JsonProperty("result")]
        public string Result { get; set; } = string.Empty; // Processed text

        [JsonProperty("error")]
        public string? Error { get; set; } // Error message, if any
    }
}