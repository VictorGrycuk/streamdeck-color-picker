using BarRaider.SdTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace StreamDeck.ColorPicker
{
    [PluginActionId("com.victorgrycuk.static.picker")]
    public class StaticPicker : PluginBase
    {
        private class PluginSettings
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
            
            public ValueFormat.ValueType ValueToShow { get; set; }

            [JsonProperty(PropertyName = "copyToClipboard")]
            public bool CopyToClipboard { get; set; }

            [JsonProperty(PropertyName = "functionType")]
            public string SelectFunctionType { get; set; }

            public FunctionType FunctionType { get; set; }

            public string ColorValue { get; set; }
        }

        public enum FunctionType
        {
            OnKeyPress,
            Dynamic
        }

        #region Private Members

        private static PluginSettings settings;

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
                if (settings.FunctionType == FunctionType.OnKeyPress)
                {
                    SetImage();
                }

                if (settings.CopyToClipboard)
                {
                    ClipboardHelper.SendToClipboard(settings.ColorValue);
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(TracingLevel.INFO, ex.Message);
            }
        }

        public void SetImage()
        {
            var position = ScreenHelper.GetMouseLocation();
            var color = ScreenHelper.GetColor(position);
            var valueFormat = new ValueFormat(settings.ValueToShow, color);
            settings.ColorValue = valueFormat.ValueToCopy;

            Connection.SetImageAsync(ScreenHelper.GetKeyImage(valueFormat));
        }

        public override void KeyReleased(KeyPayload payload) { }

        public override void OnTick()
        {
            try
            {
                if (settings.FunctionType == FunctionType.Dynamic)
                {
                    SetImage();
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(TracingLevel.INFO, ex.Message);
            }
        }

        public override void ReceivedSettings(ReceivedSettingsPayload payload)
        {
            Tools.AutoPopulateSettings(settings, payload.Settings);
            UpdateSettingsEnum();

            SaveSettings();
        }

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload) { }

        #region Private Methods

        private void SaveSettings()
        {
            _ = Connection.SetSettingsAsync(JObject.FromObject(settings));
        }

        private void UpdateSettingsEnum()
        {
            _ = Enum.TryParse(settings.SelectFunctionType, true, out FunctionType functionType);
            _ = Enum.TryParse(settings.SelectValueToShow, true, out ValueFormat.ValueType valueType);

            settings.FunctionType = functionType;
            settings.ValueToShow = valueType;
        }

        #endregion
    }
}