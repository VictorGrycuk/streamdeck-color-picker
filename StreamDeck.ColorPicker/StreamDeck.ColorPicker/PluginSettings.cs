using Newtonsoft.Json;
using static StreamDeck.ColorPicker.FormatFactory;

namespace StreamDeck.ColorPicker
{
    internal class PluginSettings
    {
        public static PluginSettings CreateDefaultSettings()
        {
            var instance = new PluginSettings
            {
                SelectValueToShow = string.Empty,
                SelectFunctionType = string.Empty,
            };

            return instance;
        }

        [JsonProperty(PropertyName = "valueToShow")]
        public string SelectValueToShow { get; set; }

        public ValueType ValueToShow { get; set; }

        [JsonProperty(PropertyName = "copyToClipboard")]
        public bool CopyToClipboard { get; set; }

        [JsonProperty(PropertyName = "functionType")]
        public string SelectFunctionType { get; set; }

        public FunctionType FunctionType { get; set; }
    }
}
