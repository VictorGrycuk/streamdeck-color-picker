using Newtonsoft.Json;

namespace StreamDeck.ColorPicker
{
    internal class PaletteSettings
    {
        public static PaletteSettings CreateDefaultSettings()
        {
            var instance = new PaletteSettings
            {
                Value = string.Empty
            };

            return instance;
        }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}
