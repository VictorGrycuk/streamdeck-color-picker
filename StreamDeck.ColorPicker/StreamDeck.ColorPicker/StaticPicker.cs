using BarRaider.SdTools;
using Newtonsoft.Json.Linq;
using StreamDeck.ColorPicker.Models;
using System;

namespace StreamDeck.ColorPicker
{
    [PluginActionId("com.victorgrycuk.static.picker")]
    public class StaticPicker : PluginBase
    {
        private Picker colorPicker;

        #region Private Members

        private static PluginSettings settings;

        #endregion
        public StaticPicker(SDConnection connection, InitialPayload payload) : base(connection, payload)
        {
            settings = payload.Settings == null || payload.Settings.Count == 0
                ? PluginSettings.CreateDefaultSettings()
                : payload.Settings.ToObject<PluginSettings>();

            SetPicker();
        }

        public override void Dispose()
        {
            Logger.Instance.LogMessage(TracingLevel.INFO, $"Destructor called");
        }

        public override void KeyPressed(KeyPayload payload) => UpdateKey(onTick: false);

        public override void KeyReleased(KeyPayload payload) { }

        public override void OnTick() => UpdateKey(onTick: true);

        public void UpdateKey(bool onTick)
        {
            try
            {
                if (onTick) colorPicker.OnTick();
                else colorPicker.OnPress();
            }
            catch (Exception ex)
            {
                Logger.Instance.LogMessage(TracingLevel.ERROR, ex.Message);
                Logger.Instance.LogMessage(TracingLevel.ERROR, ex.StackTrace);
            }
        }

        public override void ReceivedSettings(ReceivedSettingsPayload payload)
        {
            Tools.AutoPopulateSettings(settings, payload.Settings);
            UpdateSettingsEnum();
            SetPicker();

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
            _ = Enum.TryParse(settings.SelectValueToShow, true, out FormatFactory.ValueType valueType);

            settings.FunctionType = functionType;
            settings.ValueToShow = valueType;
        }

        private void SetPicker()
        {
            colorPicker = ColorPickerFactory.GetColorPicker(Connection, settings);
        }

        #endregion
    }
}