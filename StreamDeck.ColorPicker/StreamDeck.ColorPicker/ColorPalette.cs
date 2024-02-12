using BarRaider.SdTools;
using Newtonsoft.Json.Linq;
using StreamDeck.ColorPicker.Helpers;
using StreamDeck.ColorPicker.Models;
using System;
using System.Diagnostics;
using System.Drawing;

namespace StreamDeck.ColorPicker
{
    [PluginActionId("com.victorgrycuk.color.palette")]
    class ColorPalette : PluginBase
    {
        private readonly SDConnection connection;
        private static PaletteSettings settings;
        private readonly Format format;
        private readonly Stopwatch timer;

        public ColorPalette(SDConnection connection, InitialPayload payload) : base(connection, payload)
        {
            this.connection = connection;
            settings = payload.Settings == null || payload.Settings.Count == 0
                ? PaletteSettings.CreateDefaultSettings()
                : payload.Settings.ToObject<PaletteSettings>();
            format = new FormatHex();
            timer = new Stopwatch();
            SetColor(GetStoredColor());
        }

        public override void Dispose() { }

        public override void KeyPressed(KeyPayload payload)
        {
            timer.Start();
        }

        public override void KeyReleased(KeyPayload payload)
        {
            timer.Stop();
            if (timer.ElapsedMilliseconds < 250) RegularKeyPress();
            else LongKeyPress();
            timer.Reset();
        }

        public override void OnTick() { }

        public override void ReceivedGlobalSettings(ReceivedGlobalSettingsPayload payload)
        {
            SetColor(GetStoredColor());
        }

        public override void ReceivedSettings(ReceivedSettingsPayload payload)
        {
            SetColor(GetStoredColor());
        }

        private void RegularKeyPress()
        {
            var pixelColor = ScreenHelper.GetPixelColor(ScreenHelper.GetMouseLocation());
            SetColor(pixelColor);
            CopyColorToClipboard(pixelColor);

            _ = Connection.SetSettingsAsync(JObject.FromObject(settings));
        }

        private void LongKeyPress()
        {
            CopyColorToClipboard(GetStoredColor());
            Connection.ShowOk();
        }

        private Color GetStoredColor()
        {
            if (settings.Value == null) return Color.Empty;
            try
            {
                Logger.Instance.LogMessage(TracingLevel.INFO, format.GetColorFromString(settings.Value).ToArgb().ToString());
                return format.GetColorFromString(settings.Value);
            }
            catch (Exception) { return Color.Empty; }
        }

        private void SetColor(Color color)
        {
            var keyImage = ImageHelper.GetImage(color);
            var colorValue = format.GetValueToShow(color);
            var isDarkColor = ColorHelper.IsDarkColor(color);
            keyImage = ImageHelper.SetImageText(keyImage, colorValue, format, isDarkColor);
            connection.SetImageAsync(keyImage, true).Wait();
            
        }

        private void CopyColorToClipboard(Color color)
        {
            var value = format.GetValueToCopy(color);
            settings.Value = value;
            ClipboardHelper.SendToClipboard(value);
        }
    }
}
