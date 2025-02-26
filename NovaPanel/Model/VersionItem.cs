using Newtonsoft.Json;

namespace NovaPanel.Model
{
    public class VersionItem
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("ver")]
        public string? Version { get; set; }

        [JsonProperty("date")]
        public string? Update { get; set; }

        [JsonProperty("dev")]
        public string[] ?Devs { get; set; }

        [JsonProperty("data")]
        public string[]? ChangeLog { get; set; }
    }
}
