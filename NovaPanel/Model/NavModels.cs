
using System.Text.Json.Serialization;

namespace NovaPanel.Model
{
    public class NavItem
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("jump")]
        public string Jump { get; set; }

        [JsonPropertyName("tooltip")]
        public string ToolTip { get; set; }

        [JsonPropertyName("items")]
        public NavItem[]? Items { get; set; }

        [JsonPropertyName("mobile")]
        public bool Mobile { get; set; }
    }
}
