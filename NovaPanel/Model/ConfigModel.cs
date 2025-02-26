using Newtonsoft.Json;

namespace NovaPanel.Model
{
    public class ConfigModel
    {
        [JsonProperty("domain")]
        public string[] Domain { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("theme")]
        public string Theme { get; set; }

    }
}
