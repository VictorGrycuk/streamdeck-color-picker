using BarRaider.SdTools;
using StreamDeck.ColorPicker.Helpers;
using System.Drawing;
using static StreamDeck.ColorPicker.FormatFactory;

namespace StreamDeck.ColorPicker.Models
{
    internal abstract class Picker
    {
        protected readonly SDConnection connection;
        protected readonly bool copyToClipboard;
        internal readonly Format format;
        protected Point mouseLocation;

        internal Picker(SDConnection connection, ValueType valueType, bool copyToClipboard)
        {
            this.connection = connection;
            this.copyToClipboard = copyToClipboard;
            format = GetFormat(valueType);
        }

        internal virtual void OnTick()
        {
            
        }

        internal virtual void OnPress()
        {
            
        }

        protected void SetImageKey()
        {
            var pixelColor = ScreenHelper.GetPixelColor(mouseLocation);
            var keyImage = ImageHelper.GetImage(pixelColor);
            var colorValue = format.GetValueToShow(pixelColor);
            var isDarkColor = ColorHelper.IsDarkColor(pixelColor);
            keyImage = ImageHelper.SetImageText(keyImage, colorValue, format, isDarkColor);
            connection.SetImageAsync(keyImage);

            if (copyToClipboard)
            {
                ClipboardHelper.SendToClipboard(format.GetValueToCopy(pixelColor));
            }
        }
    }
}
