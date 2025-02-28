using Newtonsoft.Json;

namespace NovaPanel.Model
{
    public class Environments
    {
        [JsonProperty("net")]
        public string Net { get; set; }

        [JsonProperty("php")]
        public string PHP { get; set; }

        [JsonProperty("gcc")]
        public string GCC { get; set; }

        [JsonProperty("py")]
        public string Python { get; set; }

        [JsonProperty("node")]
        public string NodeJS { get; set; }
    }

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

        [JsonProperty("env")]
        public Environments EnvironmentPath { get; set; }

    }
}
