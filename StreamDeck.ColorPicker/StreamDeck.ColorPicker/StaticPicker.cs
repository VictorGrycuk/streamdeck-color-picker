using BarRaider.SdTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace StreamDeck.ColorPicker
{
    [PluginActionId("com.victorgrycuk.static.picker")]
    public class StaticPicker : PluginBase
    {
        private class PluginSettings
        {
            public static PluginSettings CreateDefaultSettings()
            {
                var instance = new PluginSettings { ValueToShow = string.Empty };
                return instance;
            }

            [JsonProperty(PropertyName = "valueToShow")]
            public string ValueToShow { get; set; }

            [JsonProperty(PropertyName = "copyToClipboard")]
            public bool CopyToClipboard { get; set; }
        }

        #region Private Members

        private PluginSettings settings;

        #endregion
        public StaticPicker(SDConnection connection, InitialPayload payload) : base(connection, payload)
        {
            settings = payload.Settings == null || payload.Settings.Count == 0
                ? PluginSettings.CreateDefaultSettings()
                : payload.Settings.ToObject<PluginSettings>();
        }

        public override void Dispose()
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Destructor called");
        }

        public override void KeyPressed(KeyPayload payload)
        {
            try
            {
                Enum.TryParse(settings.ValueToShow, true, out ValueFormat.ValueType valueType);
                var position = ScreenHelper.GetMouseLocation();
                var color = ScreenHelper.GetColor(position);
                var valueFormat = new ValueFormat(valueType, color);
                var keyImage = ScreenHelper.GetKeyImage(valueFormat);
                Connection.SetImageAsync(keyImage);

                if (settings.CopyToClipboard)
                {
                    ClipboardHelper.SendToClipboard(valueFormat.ValueToCopy);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(TracingLevel.INFO, ex.Message);
            }
        }

        public override void KeyReleased(KeyPayload payload) { }

        public override void OnTick() { }

        public override void ReceivedSettings(ReceivedSettingsPayload payload)
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, payload.Settings.ToString());
            Tools.AutoPopulateSettings(settings, payload.Settings);
            SaveSettings();
        }

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }

        #region Private Methods

        private Task SaveSettings()
        {
            return Connection.SetSettingsAsync(JObject.FromObject(settings));
        }

        #endregion
    }
}